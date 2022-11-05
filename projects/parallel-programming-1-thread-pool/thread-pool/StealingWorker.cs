namespace thread_pool;

public class StealingWorker: BaseWorker
{
    public StealingWorker(IReadOnlyDictionary<int, LinkedList<IExecutable>> queues, CancellationToken token): base(queues, token)
    {
    }

    public override void Run()
    {
        int selfId = Thread.CurrentThread.ManagedThreadId;
        IExecutable? task;
            
        task = PopBottom(selfId);

        while (!_token.IsCancellationRequested) 
        {
            while (task != null)
            {
                TryExecuteTask(task);
                task = PopBottom(selfId);
            }

            while (task == null && !_token.IsCancellationRequested)
            {
                Thread.Yield();
                int target = _queues.Keys.ToList()[_random.Next(_queues.Count)];
                task = PopTop(target);
            }
        }
        
        ClearQueue(selfId);
    }

    private IExecutable? PopBottom(int id)
    {
        IExecutable? task;
        lock (_queues[id])
        {
            task = _queues[id].First?.Value;
            if (task != null)
                _queues[id].RemoveFirst();
        }

        return task;
    }

    private IExecutable? PopTop(int id)
    {
        IExecutable? task;
        lock (_queues[id])
        {
            task = _queues[id].Last?.Value;
            if (task != null)
                _queues[id].RemoveLast();
        }

        return task;
    }
}