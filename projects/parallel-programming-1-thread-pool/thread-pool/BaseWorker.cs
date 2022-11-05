namespace thread_pool;

public abstract class BaseWorker
{
    protected readonly IReadOnlyDictionary<int, LinkedList<IExecutable>> _queues;

    protected readonly CancellationToken _token;

    protected readonly Random _random = new();

    public abstract void Run();
    
    public BaseWorker(IReadOnlyDictionary<int, LinkedList<IExecutable>> queues, CancellationToken token)
    {
        _queues = queues;
        _token = token;
    }
    
    protected void ClearQueue(int selfId)
    {
        var myQueue = _queues[selfId];
        lock (myQueue)
        {
            foreach (var e in myQueue)
            {
                e.Cancel();
            }

            myQueue.Clear();
        }
    }

    protected void TryExecuteTask(IExecutable? task)
    {
        int selfId = Thread.CurrentThread.ManagedThreadId;
        if (task == null || task.IsCanceled)
        {
            return;
        }
        
        if (task.Parent != null && !task.Parent.IsCompleted)
        {
            lock (_queues[selfId])
            {
                _queues[selfId].AddLast(task);
            }

            return;
        }

        task.Execute();
    }
}