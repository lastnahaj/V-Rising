using SanguineRelay.Core;

namespace SanguineRelay.Tests;

public sealed class GameThreadDispatcherTests
{
    [Fact]
    public async Task ExecutesSuccessfullyWhenDrained()
    {
        using var dispatcher = new GameThreadDispatcher(4);
        var task = dispatcher.InvokeAsync(() => 42);

        dispatcher.Drain();

        Assert.Equal(42, await task);
        Assert.Equal(new GameThreadDispatcherMetrics(0, 0, 1, 0, false), dispatcher.Metrics);
    }

    [Fact]
    public async Task PropagatesOperationExceptions()
    {
        using var dispatcher = new GameThreadDispatcher();
        var task = dispatcher.InvokeAsync<int>(() => throw new InvalidOperationException("failure"));

        dispatcher.Drain();

        await Assert.ThrowsAsync<InvalidOperationException>(() => task);
        Assert.Equal(0, dispatcher.Metrics.Pending);
    }

    [Fact]
    public async Task CancellationCompletesBeforeDrainAndSkipsWork()
    {
        using var dispatcher = new GameThreadDispatcher();
        using var cancellation = new CancellationTokenSource();
        var executed = false;
        var task = dispatcher.InvokeAsync(() => executed = true, cancellation.Token);

        cancellation.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => task);
        Assert.Equal(1, dispatcher.Metrics.Queued);
        dispatcher.Drain();
        Assert.False(executed);
        Assert.Equal(0, dispatcher.Metrics.Pending);
        Assert.Equal(0, dispatcher.Metrics.Queued);
    }

    [Fact]
    public async Task CancellationRacingWithDrainCompletesExactlyOnce()
    {
        for (var iteration = 0; iteration < 100; iteration++)
        {
            using var dispatcher = new GameThreadDispatcher();
            using var cancellation = new CancellationTokenSource();
            var executions = 0;
            var task = dispatcher.InvokeAsync(() => Interlocked.Increment(ref executions), cancellation.Token);

            Parallel.Invoke(cancellation.Cancel, () => dispatcher.Drain());

            try
            {
                await task;
            }
            catch (OperationCanceledException)
            {
            }

            Assert.InRange(executions, 0, 1);
            Assert.Equal(0, dispatcher.Metrics.Pending);
            Assert.Equal(0, dispatcher.Metrics.Queued);
            Assert.Equal(1, dispatcher.Metrics.Accepted);
        }
    }

    [Fact]
    public async Task RejectsWorkAtCapacityAndTracksRejection()
    {
        using var dispatcher = new GameThreadDispatcher(1);
        var accepted = dispatcher.InvokeAsync(() => 1);
        var rejected = dispatcher.InvokeAsync(() => 2);

        await Assert.ThrowsAsync<GameThreadQueueFullException>(() => rejected);
        Assert.Equal(new GameThreadDispatcherMetrics(1, 1, 1, 1, false), dispatcher.Metrics);

        dispatcher.Drain();
        Assert.Equal(1, await accepted);
    }

    [Fact]
    public async Task ShutdownCancelsPendingWorkAndRejectsNewWork()
    {
        using var dispatcher = new GameThreadDispatcher(2);
        var pending = dispatcher.InvokeAsync(() => 1);

        dispatcher.Shutdown();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => pending);
        await Assert.ThrowsAsync<ObjectDisposedException>(() => dispatcher.InvokeAsync(() => 2));
        Assert.Equal(new GameThreadDispatcherMetrics(0, 0, 1, 1, true), dispatcher.Metrics);
    }
}
