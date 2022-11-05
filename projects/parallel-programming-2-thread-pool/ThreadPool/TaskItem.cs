using System;
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
    public class TaskItem:IDisposable
    {
        /// <summary>
        /// Initial one wait TaskItem
        /// </summary>
        /// <param name="id">Unique thread id</param>
        /// <param name="handle">Thread completing, call this handle.</param>
        public TaskItem(int id, Action<TaskItem> handle)
        {
            this.id = id;
            completedHandle = handle;
            InitialThread();
        }
        private Thread thread;
        private Action action;
        private int id;
        private ManualResetEvent signal;
        private Action<TaskItem> completedHandle;
        private bool isWorking = false;
        /// <summary>
        /// Thread unique identifier.
        /// </summary>
        public int Id { get { return id; } }

        public bool IsAlive { get { return thread.IsAlive; } }
        public bool IsWorking { get { return isWorking; } }

        /// <summary>
        /// Start the thread with action.
        /// </summary>
        /// <param name="action">Action will be executed.</param>
        public void Start(Action action)
        {
            this.action = action;
            signal.Set();
        }

        /// <summary>
        /// If there are new tasks in TaskPool, call this method start new task without initial TaskItem.
        /// </summary>
        /// <param name="action">Action will be executed.</param>
        public void StartNew(Action action)
        {
            thread = new Thread(new ThreadStart(action));
            thread.IsBackground = true;
            signal.Reset();
            thread.Start();
            this.action = action;
            signal.Set();
            isWorking = true;
        }

        /// <summary>
        /// Initial thread that will execute the action.
        /// </summary>
        private void InitialThread()
        {
            thread = new Thread(new ThreadStart(Working));
            thread.IsBackground = true;
            signal = new ManualResetEvent(false);
            thread.Start();
        }

        /// <summary>
        /// Wrapper the action that will be executed.
        /// </summary>
        private void Working()
        {
                signal.WaitOne();
                action();
                isWorking = false;
                completedHandle?.Invoke(this);
            
        }
        /// <summary>
        /// Abort from this thread after it completes the current task,
        /// then clear thread resources.
        /// </summary>
        public void Abort()
        {
            if (thread.IsAlive && IsWorking)
                thread.Join();

            thread = null;
        }

        public void Dispose()
        {
            Abort();
        }
    }
}
