namespace thread_pool;

public interface IExecutable
{
    bool IsCompleted { get; }
    bool IsCanceled { get; }
    IExecutable? Parent { get; }
    void Execute();
    void Cancel();
}

public interface IMyTask<out TResult> : IExecutable
{
    TResult Result { get; }
    IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> f);
}