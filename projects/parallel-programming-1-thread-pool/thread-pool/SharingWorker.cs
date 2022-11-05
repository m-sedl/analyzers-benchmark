namespace thread_pool;

public class SharingWorker: BaseWorker
{
    private readonly int MAX_DIFF = 5;

    public SharingWorker(IReadOnlyDictionary<int, LinkedList<IExecutable>> queues, CancellationToken token): base(queues, token)
    {
    }
    
    public override void Run()
    {
        int selfId = Thread.CurrentThread.ManagedThreadId;
        while (!_token.IsCancellationRequested)
        {
            IExecutable? task;
            
            lock (_queues[selfId])
            {
                task = _queues[selfId].First?.Value;
                if (task != null) 
                    _queues[selfId].RemoveFirst();
            }

            TryExecuteTask(task);

            int size = _queues[selfId].Count;
            if (_random.Next(size + 1) == size)
            {
                int target = _queues.Keys.ToList()[_random.Next(_queues.Count)];
                var (fst, snd) = selfId <= target ? (selfId, target) : (target, selfId);
                lock (_queues[fst])
                {
                    lock (_queues[snd])
                    {
                        Balance(_queues[fst], _queues[snd]);
                    }
                }
            }
            
        }
        
        ClearQueue(selfId);
    }

    private void Balance(LinkedList<IExecutable> q0, LinkedList<IExecutable> q1)
    {
        if (_token.IsCancellationRequested)
        {
            return;
        }
        var (fstQ, sndQ) = q0.Count <= q1.Count ? (q0, q1) : (q1, q0);
        if (sndQ.Count - fstQ.Count > MAX_DIFF)
        {
            while (sndQ.Count > fstQ.Count)
            {
                var head = sndQ.First?.Value;
                if (sndQ.Count > 0)
                    sndQ.RemoveFirst();
                if (head != null) fstQ.AddLast(head);
            }
        }
    }
}