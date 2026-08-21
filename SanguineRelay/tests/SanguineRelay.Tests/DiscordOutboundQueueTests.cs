using BepInEx.Logging;
using SanguineRelay.Discord;

namespace SanguineRelay.Tests;

public sealed class DiscordOutboundQueueTests
{
    [Fact]
    public async Task TracksAcceptedRejectedDepthAndFailures()
    {
        var release = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        await using var queue = new DiscordOutboundQueue(new ManualLogSource("queue-tests"), 1);
        Assert.True(queue.Enqueue("running", token => release.Task.WaitAsync(token), true));

        await WaitUntilAsync(() => queue.Metrics.Depth == 0);
        Assert.True(queue.Enqueue("buffered", _ => Task.CompletedTask, true));
        Assert.False(queue.Enqueue("rejected", _ => Task.CompletedTask, true));

        var metrics = queue.Metrics;
        Assert.Equal(1, metrics.Depth);
        Assert.Equal(2, metrics.Accepted);
        Assert.Equal(1, metrics.Rejected);

        release.TrySetResult(true);
        await WaitUntilAsync(() => queue.Metrics.Depth == 0);
        await queue.FlushAsync(TimeSpan.FromSeconds(2));
        Assert.Equal(0, queue.Metrics.Depth);
    }

    [Fact]
    public async Task RetriesIdempotentFailuresAndReportsPermanentFailures()
    {
        await using var queue = new DiscordOutboundQueue(new ManualLogSource("retry-tests"), 4);
        var attempts = 0;
        Assert.True(queue.Enqueue("retry", _ =>
        {
            if (Interlocked.Increment(ref attempts) < 3)
            {
                throw new TimeoutException();
            }

            return Task.CompletedTask;
        }, true));

        var failed = 0;
        Assert.True(queue.Enqueue(
            "non-idempotent",
            _ => throw new TimeoutException(),
            false,
            () => Interlocked.Increment(ref failed)));

        await queue.FlushAsync(TimeSpan.FromSeconds(15));

        Assert.Equal(3, attempts);
        Assert.Equal(1, failed);
        Assert.Equal(2, queue.Metrics.Retries);
        Assert.Equal(1, queue.Metrics.PermanentFailures);
    }

    private static async Task WaitUntilAsync(Func<bool> condition)
    {
        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(2));
        while (!condition())
        {
            await Task.Delay(10, timeout.Token);
        }
    }
}
