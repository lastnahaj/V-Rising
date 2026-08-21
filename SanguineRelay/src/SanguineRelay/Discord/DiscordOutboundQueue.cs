using System.Threading.Channels;
using BepInEx.Logging;
using Discord.Net;

namespace SanguineRelay.Discord;

internal sealed class DiscordOutboundQueue : IAsyncDisposable
{
    private sealed record WorkItem(
        string Description,
        Func<CancellationToken, Task> Operation,
        bool IsIdempotent,
        Action? OnPermanentFailure);

    private readonly Channel<WorkItem> _queue;
    private readonly ManualLogSource _log;
    private readonly CancellationTokenSource _shutdown = new();
    private readonly Task _worker;
    private int _depth;
    private long _accepted;
    private long _rejected;
    private long _retries;
    private long _permanentFailures;
    private long _latestRateLimitObservedAt;
    private double _latestRetryAfterSeconds;

    public DiscordOutboundQueue(ManualLogSource log, int capacity = 1024)
    {
        if (capacity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity));
        }

        _log = log;
        _queue = Channel.CreateBounded<WorkItem>(new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait,
            SingleReader = true,
            SingleWriter = false
        });
        _worker = Task.Run(ProcessAsync);
    }

    public DiscordQueueMetrics Metrics => new(
        Volatile.Read(ref _depth),
        Interlocked.Read(ref _accepted),
        Interlocked.Read(ref _rejected),
        Interlocked.Read(ref _retries),
        Interlocked.Read(ref _permanentFailures));

    public bool Enqueue(
        string description,
        Func<CancellationToken, Task> operation,
        bool isIdempotent,
        Action? onPermanentFailure = null)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("A queue operation description is required.", nameof(description));
        }
        ArgumentNullException.ThrowIfNull(operation);
        if (_shutdown.IsCancellationRequested)
        {
            Reject(description);
            return false;
        }

        if (_queue.Writer.TryWrite(new WorkItem(description, operation, isIdempotent, onPermanentFailure)))
        {
            Interlocked.Increment(ref _depth);
            Interlocked.Increment(ref _accepted);
            return true;
        }

        Reject(description);
        return false;
    }

    public void ObserveRateLimit(double retryAfterSeconds)
    {
        var boundedSeconds = Math.Clamp(retryAfterSeconds, 0.25, 30);
        Volatile.Write(ref _latestRetryAfterSeconds, boundedSeconds);
        Interlocked.Exchange(ref _latestRateLimitObservedAt, Environment.TickCount64);
        Interlocked.Increment(ref _retries);
    }

    public async Task FlushAsync(TimeSpan timeout)
    {
        var completion = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        if (!Enqueue("queue flush", _ =>
            {
                completion.TrySetResult(true);
                return Task.CompletedTask;
            }, true))
        {
            throw new InvalidOperationException("The Discord outbound queue could not accept a flush marker.");
        }

        using var cancellation = new CancellationTokenSource(timeout);
        await completion.Task.WaitAsync(cancellation.Token).ConfigureAwait(false);
    }

    public async ValueTask DisposeAsync()
    {
        _queue.Writer.TryComplete();
        _shutdown.CancelAfter(TimeSpan.FromSeconds(10));
        try
        {
            await _worker.ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
            _log.LogWarning("Discord outbound queue did not drain before shutdown.");
        }
        finally
        {
            _shutdown.Cancel();
            _shutdown.Dispose();
        }
    }

    private async Task ProcessAsync()
    {
        await foreach (var item in _queue.Reader.ReadAllAsync(_shutdown.Token).ConfigureAwait(false))
        {
            Interlocked.Decrement(ref _depth);
            await ExecuteWithRetryAsync(item, _shutdown.Token).ConfigureAwait(false);
        }
    }

    private async Task ExecuteWithRetryAsync(WorkItem item, CancellationToken cancellationToken)
    {
        for (var attempt = 1; attempt <= 3; attempt++)
        {
            try
            {
                await item.Operation(cancellationToken).ConfigureAwait(false);
                return;
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                return;
            }
            catch (Exception exception) when (attempt < 3 && DiscordRetryPolicy.ShouldRetry(exception, item.IsIdempotent))
            {
                Interlocked.Increment(ref _retries);
                var delay = DiscordRetryPolicy.GetDelay(exception, attempt, GetRecentRetryAfter());
                await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                Interlocked.Increment(ref _permanentFailures);
                var detail = exception is HttpException httpException
                    ? $"HTTP {(int)httpException.HttpCode}"
                    : exception.GetType().Name;
                _log.LogError($"Discord operation '{item.Description}' failed permanently: {detail}.");
                InvokeFailureCallback(item);
                return;
            }
        }
    }

    private TimeSpan? GetRecentRetryAfter()
    {
        var observedAt = Interlocked.Read(ref _latestRateLimitObservedAt);
        if (observedAt == 0 || Environment.TickCount64 - observedAt > 60_000)
        {
            return null;
        }

        return TimeSpan.FromSeconds(Volatile.Read(ref _latestRetryAfterSeconds));
    }

    private void Reject(string description)
    {
        Interlocked.Increment(ref _rejected);
        _log.LogWarning($"Discord outbound queue rejected '{description}' because it is full or shutting down.");
    }

    private void InvokeFailureCallback(WorkItem item)
    {
        if (item.OnPermanentFailure is null)
        {
            return;
        }

        try
        {
            item.OnPermanentFailure();
        }
        catch (Exception exception)
        {
            _log.LogError($"Discord failure callback for '{item.Description}' failed: {exception.GetType().Name}: {exception.Message}");
        }
    }
}

internal sealed record DiscordQueueMetrics(
    int Depth,
    long Accepted,
    long Rejected,
    long Retries,
    long PermanentFailures);

internal static class DiscordRetryPolicy
{
    private static readonly TimeSpan MaximumDelay = TimeSpan.FromSeconds(30);

    public static bool ShouldRetry(Exception exception, bool isIdempotent) =>
        exception is RateLimitedException ||
        isIdempotent && (exception is TimeoutException or HttpRequestException ||
                         exception is HttpException httpException && (int)httpException.HttpCode >= 500);

    public static TimeSpan GetDelay(Exception exception, int attempt, TimeSpan? discordRetryAfter)
    {
        var delay = exception is RateLimitedException && discordRetryAfter.HasValue
            ? discordRetryAfter.Value
            : TimeSpan.FromSeconds(Math.Pow(2, Math.Clamp(attempt, 1, 4)));
        return delay < TimeSpan.FromMilliseconds(250)
            ? TimeSpan.FromMilliseconds(250)
            : delay > MaximumDelay ? MaximumDelay : delay;
    }
}
