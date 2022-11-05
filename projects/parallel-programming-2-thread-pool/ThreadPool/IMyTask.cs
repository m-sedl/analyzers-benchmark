using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyThreadPool
{
    /// <summary>
    /// Base task interface
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// Flag indicate if the task is completed.
        /// </summary>
        bool IsCompleted { get; }
        /// <summary>
        /// Indicate the parent task for this task.
        /// </summary>
        ITask Parent { get; set; }
        /// <summary>
        /// Start this task
        /// </summary>
        void Execute();
        AggregateException AggregateException { get;}
    }


    /// <summary>
    /// Task passed into ThreadPool
    /// </summary>
    /// <typeparam name="TResult">Type of result</typeparam>
    public interface IMyTask<TResult> :ITask
    {        
        /// <summary>
        /// Task result.
        /// </summary>
        TResult Result { get; }

        /// <summary>
        /// AggregateException when task execute false.
        /// </summary>
        

        /// <summary>
        /// Task function.
        /// </summary>
        Func<TResult> TaskItem { get; }

        /// <summary>
        /// Add continued Func<T1,T2> with this task.
        /// </summary>
        /// <typeparam name="TNewResult">New type of result.</typeparam>
        /// <param name="newFunc">New function will pass in task.</param>
        /// <returns>New task with new function.</returns>
        IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> newFunc);     
    }
}
