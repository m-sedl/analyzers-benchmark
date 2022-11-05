using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSynchronization
{
    /// <summary>
    /// Producer entity
    /// </summary>
    public class Producer
    {
        /// <summary>
        /// Initial producer instance with producer's name and share resources pool.
        /// </summary>
        /// <param name="name">Producer's name</param>
        /// <param name="queue">Public share resources pool</param>
        public Producer(string name, MessageQueue queue)
        {
            content = string.Format("From producer {0}", name);
            // initial thread with operation
            thread = new Thread(() =>
            {
                while (isWorking)
                {
                    //   Console.WriteLine($"Producer {name} Produce {content}");
                    queue.Enqueue(content);
                    Thread.Sleep(2500);
                }
                Console.WriteLine($"Producer {name} has stop.");
            });
        }
        
        private volatile bool isWorking = true;
        private Semaphore semaphore = new Semaphore(1, 1);
        private Thread thread;
        private string content;

        /// <summary>
        /// Begin producing operation
        /// </summary>
        public void Start()
        {
            // Use Semaphore to ensure atomic operation.
            semaphore.WaitOne();          
            thread.Start();
            semaphore.Release(1);
        }

        /// <summary>
        /// Stop thread
        /// </summary>
        public void Stop()
        {
            isWorking = false;
        }
    }
}
