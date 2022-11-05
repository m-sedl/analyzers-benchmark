using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyThreadPool
{
    public class MyTask<TResult> : IMyTask<TResult>
    {

        public MyTask(Func<TResult> func, MyTaskPool pool, CancellationTokenSource cancellation)
        {
            this.func = func;
            resultSignal = new ManualResetEvent(false);
            this.pool = pool;
            this.cancellation = cancellation;
        }
        private Func<TResult> func = null;
        private TResult result;
        private bool isCompleted = false;
        private ManualResetEvent resultSignal;
        private AggregateException innerException = null;
        private MyTaskPool pool;
        private CancellationTokenSource cancellation;
        private ITask parent;
        /// <summary>
        /// Task status.
        /// </summary>
        public bool IsCompleted
        {
            get
            {
                return isCompleted;
            }
        }

        public ITask Parent
        {
            get => parent;
            set => parent = value;
        }

        /// <summary>
        /// Task result, can only get after task is completed.
        /// If user cancel task, the value will be default TResult.
        /// </summary>
        public TResult Result
        {
            get
            {
                resultSignal.WaitOne();
                if (AggregateException != null)
                {
                    throw AggregateException;
                }

                return result;


            }
        }

        /// <summary>
        /// Task inner exception.
        /// </summary>
        public AggregateException AggregateException
        {
            get
            {
                return innerException;
            }
        }
        /// <summary>
        /// Function will be exected by the thread.
        /// </summary>
        public Func<TResult> TaskItem
        {
            get
            {
                return func;
            }
        }

        /// <summary>
        /// Set continue task after this task.
        /// </summary>
        /// <typeparam name="TNewResult">New type of result that the continue taks will return.</typeparam>
        /// <param name="newFunc">New task function.</param>
        /// <returns>New Task.</returns>
        public IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> newFunc)
        {
            TNewResult task() => newFunc(Result);
            var r = pool.AddTask<TNewResult>(task);
            r.Parent = this;
            return r;
        }
        private object obj = new object();
        /// <summary>
        /// Execute task.
        /// </summary>
        public void Execute()
        {        
            if (cancellation != null && cancellation.IsCancellationRequested)
            {
                innerException = new AggregateException(new OperationCanceledException("The operation has been canceled."));
                isCompleted = true;
                resultSignal.Set();
                return;
            }

            if (parent != null && parent.AggregateException != null)
            {
                innerException = new AggregateException(parent.AggregateException.Message);
                lock (obj)
                {
                    isCompleted = true;
                    resultSignal.Set();
                }              
                return;
            }

            try
            {
                if (func == null)
                {
                    innerException = new AggregateException(new ArgumentNullException("Please setting delegate method."));
                    lock (obj)
                    {
                        isCompleted = true;
                        resultSignal.Set();
                    }
                    return;
                }
                // call delegate function to calculate.
                result = func.Invoke();
                lock (obj)
                {
                    isCompleted = true;
                    resultSignal.Set();
                }
            }
            catch (Exception ex)
            {
                innerException = new AggregateException("Function is null", ex);
                lock (obj)
                {
                    isCompleted = true;
                    resultSignal.Set();
                }
            }
            
        }
    }
}
