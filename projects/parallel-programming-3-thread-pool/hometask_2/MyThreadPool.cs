namespace CustomThreading
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Diagnostics;

    /// <summary>
    /// Thread pool with fixed count of threads
    /// </summary>
    public class MyThreadPool : IDisposable
    {
        /// <summary>
        /// Contains all threads in the pool
        /// </summary>
        private readonly Thread[] threads;

        /// <summary>
        /// Cancellation token
        /// </summary>
        private readonly CancellationTokenSource cancellation =
            new CancellationTokenSource();

        /// <summary>
        /// Unlocks threads if there are tasks waiting for execution
        /// </summary>
        private readonly EventWaitHandle threadGuard;

        /// <summary>
        /// Count of live threads
        /// </summary>
        private volatile int availableThreadCount;

        /// <summary>
        /// Queue which contains tasks which are waiting to be executed
        /// </summary>
        private ConcurrentQueue<Action<bool>>[] pendingTasks;

        /// <summary>
        /// States whether we use stealing or sharing strategy
        /// </summary>
        private readonly bool shouldUseStealing;

        private const int balancingThreshold = 2;

        private readonly Object[] balancingLockObject;

        private int GetNextThreadID()
        {
            var rand = new Random();
            return rand.Next(0, availableThreadCount);
        }

        /// <summary>
        /// Swaps two ints
        /// </summary>
        static void SwapNum(ref int x, ref int y)
        {

            int tempswap = x;
            x = y;
            y = tempswap;
        }

        /// <summary>
        /// Pseudo-randomly balances two task queues
        /// </summary>
        private void RandomBalanceTasks(int current, int victim)
        {
            if (current == victim || 
                    current < 0 || current >= this.availableThreadCount || 
                    victim < 0 || victim >= this.availableThreadCount) {
                return;
            }

            var current_size = this.pendingTasks[current].Count;
            var rand = new Random();
            if (rand.Next(current_size + 1) != current_size) {
                return;
            }
            if (Math.Abs(current_size - this.pendingTasks[victim].Count) <= MyThreadPool.balancingThreshold) {
                return;
            }

            // To prevent simultaneous balancings
            var lockFirst = Math.Min(current, victim);
            var lockSecond = Math.Max(current, victim);
            lock (this.balancingLockObject[lockFirst]) {
                lock (this.balancingLockObject[lockSecond]) {
                    if (this.pendingTasks[current].Count > this.pendingTasks[victim].Count) {
                        SwapNum(ref victim, ref current);
                    }

                    while (this.pendingTasks[victim].Count > this.pendingTasks[current].Count) {
                        if (!this.pendingTasks[victim].TryDequeue(out Action<bool> balanceTask)) {
                            break;
                        }
                        this.pendingTasks[current].Enqueue(balanceTask);
                    }
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MyThreadPool"/> class.
        /// </summary>
        /// <param name="threadCount">Fixed thread count in pool</param>
        public MyThreadPool(int threadCount, bool shouldUseStealing = false)
        {
            this.shouldUseStealing = shouldUseStealing;

            this.threadGuard = new ManualResetEvent(false);

            this.MaxThreadCount = threadCount;
            this.availableThreadCount = this.MaxThreadCount;

            this.balancingLockObject = new Object[this.availableThreadCount];
            this.pendingTasks = new ConcurrentQueue<Action<bool>>[threadCount];
            for (int i = 0; i < threadCount; ++i) {
                this.pendingTasks[i] = new ConcurrentQueue<Action<bool>>();
                this.balancingLockObject[i] = new Object();
            }

            this.threads = new Thread[threadCount];
            for (int i = 0; i < this.threads.Length; ++i)
            {
                var threadNum = i;
                this.threads[i] = new Thread(() =>
                {
                    while (!this.cancellation.IsCancellationRequested)
                    {
                        if (this.pendingTasks[threadNum]
                                .TryDequeue(out Action<bool> executeTask))
                        {
                            executeTask(false);
                        }
                        else if (this.shouldUseStealing && 
                                this.pendingTasks[GetNextThreadID()]
                                    .TryDequeue(out Action<bool> executeTaskStolen)) {
                            executeTaskStolen(false);
                        } else {
                            this.threadGuard.WaitOne();

                            if (this.cancellation.IsCancellationRequested)
                            {
                                this.threadGuard.Set();
                            }
                        }

                        if (!this.shouldUseStealing) {
                            var victim = GetNextThreadID();
                            var current = threadNum;

                            if (victim != current) {
                                this.RandomBalanceTasks(current, victim);
                            }
                        }
                    }

                    --this.availableThreadCount;
                });

                this.threads[i].IsBackground = true;
                this.threads[i].Start();
            }
        }

        /// <summary>
        /// Gets maximal fixed count of threads in the pool
        /// </summary>
        public int MaxThreadCount { get; }

        /// <summary>
        /// Gets count of live treads
        /// </summary>
        public int AvailableThreadCount => this.availableThreadCount;

        /// <summary>
        /// Adds new function to be calculated by the pool threads
        /// </summary>
        /// <typeparam name="TResult">Supplier function result type</typeparam>
        /// <param name="supplier">Function to execute</param>
        /// <returns>Task for calculating function</returns>
        public IMyTask<TResult> Enqueue<TResult>(Func<TResult> supplier)
        {
            if (this.cancellation.IsCancellationRequested)
            {
                return null;
            }

            var newTask = new MyTask<TResult>(this, supplier);

            this.pendingTasks[GetNextThreadID()].Enqueue(newTask.ExecuteTaskManually);
            this.threadGuard.Set();

            return newTask;
        }

        /// <summary>
        /// Destroy all thread and stop pool work 
        /// (Previous tasks calculation will be finished)
        /// </summary>
        public void Dispose()
        {
            this.cancellation.Cancel();
            this.threadGuard.Set();

            for (var i = 0; i < this.availableThreadCount; ++i) {
                while (this.pendingTasks[i].TryDequeue(out Action<bool> taskToDisable))
                {
                    taskToDisable(true);
                }
            }

            foreach (var thread in this.threads)
            {
                if (thread.IsAlive)
                {
                    thread.Join();   
                }
            }
        }
    }
}
