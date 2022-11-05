using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSynchronization
{
    /// <summary>
    /// Public resources pool. 
    /// Producers will put msg into this pool and
    /// consumer will get msg from this pool.
    /// </summary>
    public class MessageQueue
    {
        private List<string> messages = new List<string>();

        /// <summary>
        /// Put msg into pool.
        /// </summary>
        /// <param name="msg"></param>
        public void Enqueue(string msg)
        {
            // lock this operation ensure that atomic operation.
            lock (messages)
            {
                messages.Add(msg);
            }
        }

        /// <summary>
        /// Get msg from pool.
        /// </summary>
        /// <returns></returns>
        public string Dequeue()
        {
            while (true)
            {
                lock (messages)
                {
                    if (messages.Count == 0)
                        continue;
                    var msg = messages[0];
                    messages.RemoveAt(0);
                    return msg;
                }
            }
        }
    }
}
