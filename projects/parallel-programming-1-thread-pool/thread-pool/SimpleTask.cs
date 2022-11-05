namespace thread_pool;

public class SimpleTask<T> : IMyTask<T>
{
    private readonly List<IExecutable> children = new();
    
    public IExecutable? Parent { get; private set; }

    public void Execute()
    {
        try
        {
            _result = _f.Invoke();
            _isCompleted = true;
        }
        catch (Exception ex)
        {
            lock (_exceptionLock)
            {
                _exception = new AggregateException(ex);
                children.ForEach(e => e.Cancel());
            }
        }
    }

    public void Cancel()
    {
        lock (_exceptionLock)
        {
            _exception = new Exception("Task was canceled");
        }
        children.ForEach(e => e.Cancel());
    }

    private Exception? _exception;
    private readonly object _exceptionLock = new object();

    public bool IsCanceled
    {
        get
        {
            lock (_exceptionLock)
            {
                return _exception != null;
            }
        }
    }

    private volatile bool _isCompleted;

    private T _result;

    public bool IsCompleted => _isCompleted;

    public T Result
    {
        get
        {
            while (true)
            {
                if (_isCompleted)
                {
                    return _result;
                }

                if (_exception != null)
                {
                    throw _exception;
                }

                Thread.Yield();
            }
        }
    }

    private readonly MyThreadPool _pool;
    private readonly Func<T> _f;

    public SimpleTask(MyThreadPool pool, Func<T> f)
    {
        _pool = pool;
        _f = f;
    }
    
    public IMyTask<TNewResult> ContinueWith<TNewResult>(Func<T, TNewResult> f)
    {
        var task = new SimpleTask<TNewResult>(_pool, () => f(Result))
        {
            Parent = this
        };
        children.Add(task);
        _pool.Enqueue(task);
        return task;
    }
}