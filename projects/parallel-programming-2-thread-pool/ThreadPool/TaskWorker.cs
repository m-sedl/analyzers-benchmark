using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyThreadPool
{
    /// <summary>
    /// Wrap thread wait for action.
    /// </summary>
    public class TaskWorker:IDisposable
    {
        /// <summary>
        /// Initial one wait TaskItem
        /// </summary>
        /// <param name="id">Unique thread id</param>
        /// <param name="handle">Thread completing, call this handle.</param>
        public TaskWorker(Dictionary<int,List<ITask>> tasks, CancellationTokenSource cancellation, WorkingModeEnum workingMode)
        {
            this.tasks = tasks;
            this.cancellation = cancellation;
            random = new Random();
            thread = workingMode == WorkingModeEnum.Stealing ? new Thread(StealingWorking) : new Thread(ShareWorking);
            threadId = thread.ManagedThreadId;
        }
        private Thread thread;
        private Dictionary<int, List<ITask>> tasks;
        private CancellationTokenSource cancellation;
        private int threadId;
        private Random random;
        private const int DIFF = 3;
        /// <summary>
        /// Thread uinque id in thie worker
        /// </summary>
        public int ThreadId { get => threadId; }

        /// <summary>
        /// Start the thread.
        /// </summary>
        public void Start()
        {
            thread.Start();
        }

        public void Dispose()
        {
            cancellation.Cancel();
            thread.Join();
            thread = null;
            lock (tasks[threadId])
            {
                tasks[threadId].Clear();
            }
        }
        private object obj = new object();
        /// <summary>
        /// Share working mode
        /// </summary>
        private void ShareWorking()
        {
            while (!cancellation.IsCancellationRequested)
            {
              //  ITask task;
                var thradTasks = tasks[threadId];
                lock (thradTasks)
                {
                    var task = thradTasks.FirstOrDefault();                       
                    if(task != null)
                    {
                        if (task.Parent != null && !task.Parent.IsCompleted)
                        {
                            thradTasks.Add(task);
                        }
                        else
                        {
                            task.Execute();
                        }
                            
                        thradTasks.RemoveAt(0);
                    }

                    if (tasks.Count == 1)
                        continue;
                }

                if (random.Next(tasks[threadId].Count) == tasks[threadId].Count - 1)
                {
                    var selectedKey = tasks.Keys.ToList()[random.Next(tasks.Count)];
                    var first = Math.Min(selectedKey, threadId);
                    var second = Math.Max(selectedKey, threadId);
                    lock (tasks[first])
                    {
                        lock (tasks[second])
                        {
                            var from = tasks[threadId].Count > tasks[selectedKey].Count ? tasks[threadId] : tasks[selectedKey];
                            var to = tasks[threadId].Count <= tasks[selectedKey].Count ? tasks[threadId] : tasks[selectedKey];
                            if(from.Count - to.Count > DIFF)
                            {
                                while(from.Count > to.Count)
                                {
                                    var t = from.LastOrDefault();
                                    if(t != null)
                                    {
                                        from.RemoveAt(from.Count - 1);
                                        to.Add(t);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void StealingWorking()
        {          
            while (!cancellation.IsCancellationRequested)
            {
                ITask task = null;
                lock (tasks[threadId])
                {
                    task = tasks[threadId].FirstOrDefault();
                    if (task != null)
                        tasks[threadId].RemoveAt(0);
                }
                
                while (task != null)
                {
                    
                    if (task.Parent != null && !task.Parent.IsCompleted)
                    {
                        lock(tasks[threadId])
                            tasks[threadId].Add(task);
                    }
                    else
                    {
                        task.Execute();
                    }
                    lock (tasks[threadId])
                    {
                        task = tasks[threadId].FirstOrDefault();
                        if (task != null)
                            tasks[threadId].RemoveAt(0);
                    }
                }

                // after the tasks in this thread has been executed, stealing tasks from other threads. 

                var target = tasks[tasks.Keys.ToList()[random.Next(tasks.Keys.Count)]];
                if (task == null && !cancellation.IsCancellationRequested)
                {
                    lock (target)
                    {   
                        if(target.Count == 0)
                        {
                            Thread.Yield();
                            continue;
                        }
                        task = target.LastOrDefault();
                        if (task != null)
                        {
                            target.RemoveAt(target.Count - 1);
                            
                        }
                    }
                    if (task?.Parent != null && !task.Parent.IsCompleted)
                    {
                        lock (tasks[threadId])
                            tasks[threadId].Add(task);
                    }
                    else
                    {
                        task?.Execute();
                    }
                }
            }
        }
    }
}
