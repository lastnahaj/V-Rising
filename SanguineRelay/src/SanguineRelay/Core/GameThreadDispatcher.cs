using System.Collections.Concurrent;

namespace SanguineRelay.Core;

internal sealed record GameThreadDispatcherMetrics(int Pending, int Queued, long Accepted, long Rejected, bool IsShutdown);

internal sealed class GameThreadQueueFullException : InvalidOperationException
{
    public GameThreadQueueFullException(int capacity)
        : base($"The game-thread queue reached its capacity of {capacity} operations.")
    {
    }
}

internal sealed class GameThreadDispatcher : IDisposable
{
    private interface IWorkItem
    {
        void Execute();

        void CancelForShutdown(CancellationToken cancellationToken);
    }

    private sealed class WorkItem<T> : IWorkItem
    {
        private readonly Func<T> _operation;
        private readonly TaskCompletionSource<T> _completion = new(TaskCreationOptions.RunContinuationsAsynchronously);
        private readonly Action _onTerminal;
        private readonly CancellationToken _callerToken;
        private CancellationTokenRegistration _registration;
        private int _state;

        public WorkItem(Func<T> operation, CancellationToken callerToken, Action onTerminal)
        {
            _operation = operation;
            _callerToken = callerToken;
            _onTerminal = onTerminal;
            if (callerToken.CanBeCanceled)
            {
                _registration = callerToken.UnsafeRegister(static state => ((WorkItem<T>)state!).CancelByCaller(), this);
                if (Volatile.Read(ref _state) != 0)
                {
                    _registration.Dispose();
                }
            }
        }

        public Task<T> Task => _completion.Task;

        public void Execute()
        {
            if (Interlocked.CompareExchange(ref _state, 1, 0) != 0)
            {
                _registration.Dispose();
                return;
            }

            _onTerminal();
            _registration.Dispose();
            try
            {
                _completion.TrySetResult(_operation());
            }
            catch (Exception exception)
            {
                _completion.TrySetException(exception);
            }
        }

        public void CancelForShutdown(CancellationToken cancellationToken)
        {
            if (Interlocked.CompareExchange(ref _state, 2, 0) == 0)
            {
                _onTerminal();
                _completion.TrySetCanceled(cancellationToken);
            }

            _registration.Dispose();
        }

        private void CancelByCaller()
        {
            if (Interlocked.CompareExchange(ref _state, 2, 0) != 0)
            {
                return;
            }

            _onTerminal();
            _completion.TrySetCanceled(_callerToken);
        }
    }

    private readonly ConcurrentQueue<IWorkItem> _queue = new();
    private readonly object _lifecycleGate = new();
    private readonly CancellationTokenSource _shutdown = new();
    private readonly int _capacity;
    private int _pending;
    private int _queued;
    private int _isShutdown;
    private long _accepted;
    private long _rejected;

    public GameThreadDispatcher(int capacity = 1024)
    {
        if (capacity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity));
        }

        _capacity = capacity;
    }

    public GameThreadDispatcherMetrics Metrics => new(
        Volatile.Read(ref _pending),
        Volatile.Read(ref _queued),
        Interlocked.Read(ref _accepted),
        Interlocked.Read(ref _rejected),
        Volatile.Read(ref _isShutdown) != 0);

    public Task<T> InvokeAsync<T>(Func<T> operation, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(operation);
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled<T>(cancellationToken);
        }

        lock (_lifecycleGate)
        {
            if (_isShutdown != 0)
            {
                Interlocked.Increment(ref _rejected);
                return Task.FromException<T>(new ObjectDisposedException(nameof(GameThreadDispatcher)));
            }

            if (_queued >= _capacity)
            {
                Interlocked.Increment(ref _rejected);
                return Task.FromException<T>(new GameThreadQueueFullException(_capacity));
            }

            Interlocked.Increment(ref _pending);
            Interlocked.Increment(ref _queued);
            var item = new WorkItem<T>(operation, cancellationToken, () => Interlocked.Decrement(ref _pending));
            _queue.Enqueue(item);
            Interlocked.Increment(ref _accepted);
            return item.Task;
        }
    }

    public Task InvokeAsync(Action operation, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(operation);
        return InvokeAsync(() =>
        {
            operation();
            return true;
        }, cancellationToken);
    }

    public void Drain(int maximumActions = 128)
    {
        if (maximumActions <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maximumActions));
        }

        for (var count = 0; count < maximumActions && _queue.TryDequeue(out var item); count++)
        {
            Interlocked.Decrement(ref _queued);
            item.Execute();
        }
    }

    public void Shutdown()
    {
        lock (_lifecycleGate)
        {
            if (Interlocked.Exchange(ref _isShutdown, 1) != 0)
            {
                return;
            }

            _shutdown.Cancel();
        }

        while (_queue.TryDequeue(out var item))
        {
            Interlocked.Decrement(ref _queued);
            item.CancelForShutdown(_shutdown.Token);
        }
    }

    public void Dispose()
    {
        Shutdown();
        _shutdown.Dispose();
    }
}
