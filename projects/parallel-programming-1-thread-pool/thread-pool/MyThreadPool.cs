using System.Collections.Concurrent;

namespace thread_pool;

public class MyThreadPool: IDisposable
{
    private readonly int _threadsCount;

    private readonly Dictionary<int, LinkedList<IExecutable>> _queues;

    private readonly List<Thread> _threads = new();

    private  readonly  CancellationTokenSource _source = new();

    private readonly Random _random = new();

    public int ThreadsCount => _threads.Count(t => t.IsAlive);
    public MyThreadPool(int threadsCount, bool useSharing)
    {
        _threadsCount = threadsCount;
        _queues = new Dictionary<int, LinkedList<IExecutable>>(_threadsCount);
        for (int i = 0; i < _threadsCount; i++)
        {
            BaseWorker worker = useSharing ? new SharingWorker(_queues, _source.Token) : new StealingWorker(_queues, _source.Token);
            var thread = new Thread(worker.Run);
            _queues[thread.ManagedThreadId] = new LinkedList<IExecutable>();
            _threads.Add(thread);
        }
        
        _threads.ForEach(t => t.Start());
    }
    
    public bool Enqueue(IExecutable e)
    {
        var idx = _queues.Keys.ToList()[_random.Next(_threadsCount)];
        lock (_source)
        {
            if (_source.IsCancellationRequested)
            {
                return false;
            }

            lock (_queues[idx])
            {
                _queues[idx].AddLast(e);
            }
        }
        return true;
    }

    public void Dispose()
    {
        lock (_source)
        {
            _source.Cancel();
        }
        _threads.ForEach(t => t.Join());
    }
}