namespace CustomThreading
{
    using System;

    /// <summary>
    /// Provides interface for task used by <see cref="MyThreadPool"/>
    /// </summary>
    /// <typeparam name="TResult">Type of task result</typeparam>
    public interface IMyTask<TResult>
    {
        /// <summary>
        /// Gets a value indicating whether the task is completed
        /// </summary>
        bool IsCompleted { get; }

        /// <summary>
        /// Gets task result. Blocks caller thread until result is ready
        /// </summary>
        TResult Result { get; }

        /// <summary>
        /// Generates new task based on the result of current task
        /// </summary>
        /// <typeparam name="TResultNew">New task result type</typeparam>
        /// <param name="newSupplier">New supplier function</param>
        /// <returns>
        /// Task which executes new supplier function
        /// with the result of current task as a parameter
        /// </returns>
        IMyTask<TResultNew> ContinueWith<TResultNew>(Func<TResult, TResultNew> newSupplier);
    }
}
