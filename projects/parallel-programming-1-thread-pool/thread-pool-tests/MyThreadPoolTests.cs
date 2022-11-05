using thread_pool;

namespace thread_pool_tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    private void RunSingleTask(bool useSharing)
    {
        var pool = new MyThreadPool(4, useSharing);
        var task = new SimpleTask<int>(pool, () =>
        {
            Thread.Sleep(1000);
            return 42;
        });
        Assert.IsTrue(pool.Enqueue(task));
        Assert.AreEqual(42, task.Result);
        pool.Dispose();
    }
    
    [Test]
    public void SingleTaskTest()
    {
        RunSingleTask(true);
        RunSingleTask(false);
    }

    private void RunMultipleTask(bool useSharing)
    {
        var pool = new MyThreadPool(4, useSharing);

        var tasks = new List<IMyTask<int>>(20);
        for (int i = 0; i < 20; i++)
        {
            int iCopy = i;
            var t = new SimpleTask<int>(pool, () =>
            {
                Thread.Sleep(500);
                return iCopy;
            });
            pool.Enqueue(t);
            tasks.Add(t);
        }

        for (int i = 0; i < tasks.Count; i++)
        {
            Assert.AreEqual(i, tasks[i].Result);
        }
        
        pool.Dispose();
    }
    
    [Test]
    public void MultipleTaskTest()
    {
        RunMultipleTask(true);
        RunMultipleTask(false);
    }

    void RunContinueWith(bool useSharing)
    {
        var pool = new MyThreadPool(4, useSharing);
        var task = new SimpleTask<int>(pool, () =>
        {
            Thread.Sleep(1000);
            return 42;
        });
        pool.Enqueue(task);
        var r = task.ContinueWith(a =>
        {
            Thread.Sleep(200);
            return a + 1;
        }).ContinueWith(b => b * b).Result;
        Assert.AreEqual(43 * 43, r);
        pool.Dispose();
    }
    
    [Test]
    public void ContinueWithTest()
    {
        RunContinueWith(true);
        RunContinueWith(false);
    }
    
    void RunExceptionTask(bool useSharing)
    {
        var pool = new MyThreadPool(4, useSharing);
        var task = new SimpleTask<int>(pool, () =>
        {
            Thread.Sleep(1000);
            throw new Exception("Alarm");
        });
        pool.Enqueue(task);
        Assert.Throws(typeof(AggregateException), () =>
        {
            var b = task.Result;
        });
        pool.Dispose();
    }
    
    [Test]
    public void ExceptionTest()
    {
        RunExceptionTask(true);
        RunExceptionTask(false);
    }
    
    void RunDisposeTest(bool useSharing)
    {
        var pool = new MyThreadPool(4, useSharing);
        pool.Dispose();
        
        var t = new SimpleTask<int>(pool, () => 42);
        Assert.IsFalse(pool.Enqueue(t));
    }
    
    [Test]
    public void DisposeTest()
    {
        RunDisposeTest(true);
        RunDisposeTest(false);
    }
    
    private void RunThreadsCountTest(bool useSharing)
    {
        var pool = new MyThreadPool(4, useSharing);
        Assert.AreEqual(4, pool.ThreadsCount);
        pool.Dispose();
    }
    
    [Test]
    public void ThreadsCountTest()
    {
        RunThreadsCountTest(true);
        RunThreadsCountTest(false);
    }
}