using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyThreadPool
{
    public class MyTaskPool : IDisposable
    {
        /// <summary>
        /// Initial task pool with thread nums.
        /// </summary>
        /// <param name="threadNum"></param>
        public MyTaskPool(int threadNum, WorkingModeEnum workingMode)
        {
            InitialThreads(threadNum, workingMode);           
            random = new Random();
        }
        private int threadCount;
        private CancellationTokenSource cancellation;
        private Dictionary<int,List<ITask>> waitQueue;
        private List<TaskWorker> workers;
        private Random random;
        /// <summary>
        /// Initial task pool.
        /// </summary>
        private void InitialThreads(int threadNum, WorkingModeEnum workingMode)
        {
            threadCount = threadNum;
            cancellation = new CancellationTokenSource();
            waitQueue = new Dictionary<int, List<ITask>>();
            workers = new List<TaskWorker>();
            // Initial threads and the task queue for this thread.
                for (int i = 0; i < threadNum; i++)
                {
                    var taskworker = new TaskWorker(waitQueue, cancellation, workingMode);
                    waitQueue.Add(taskworker.ThreadId, new List<ITask>());
                    workers.Add(taskworker);
                }

                foreach (var worker in workers)
                    worker.Start();
            
        }

        /// <summary>
        /// Max count of threads in thread pool.
        /// </summary>
        public int ThreadCount
        {
            get
            {
                return threadCount;
            }
        }

        /// <summary>
        /// Add new Func<T> into task pool
        /// </summary>
        /// <typeparam name="TResult">Result of Type</typeparam>
        /// <param name="func">Function will be executed.</param>
        /// <returns></returns>
        public IMyTask<TResult> AddTask<TResult>(Func<TResult> func)
        {
            var index = random.Next(0, waitQueue.Keys.Count);
            var key = waitQueue.Keys.ToList()[index];
            lock (waitQueue[key])
            {
                if (cancellation != null && cancellation.IsCancellationRequested)
                    return null;

                var task = new MyTask<TResult>(func, this, cancellation);
                
                waitQueue[key].Add(task);
                return task;
            }
        }

        /// <summary>
        /// Shut down the pool, cancel wait tasks and wait the working tasks completed.
        /// </summary>
        public void ShutDown()
        {

            cancellation.Cancel();
            foreach (var worker in workers)
                worker.Dispose();
        }

        public void Dispose()
        {
            ShutDown();
            threadCount = 0;
        }
    }

    /// <summary>
    /// Thread working mode
    /// </summary>
    public enum WorkingModeEnum
    {
        Share,
        Stealing
    }
}
