using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSynchronization
{
    /// <summary>
    /// Consumer entity that will get msg from public resources pool.
    /// </summary>
    public class Consumer
    {
        /// <summary>
        /// Create consumer instance with name and public share pool entity.
        /// </summary>
        /// <param name="name">Comsumer's name</param>
        /// <param name="queue">Share resources pool</param>
        public Consumer(string name, MessageQueue queue)
        {
            semamphore = new Semaphore(1,1);

            // initial thread with operation
            thread = new Thread(() =>
            {
                while (isWorking)
                {
                    Console.WriteLine($"Consumer {name} receive from: {queue.Dequeue()}");
                    Thread.Sleep(2500);
                }
                Console.WriteLine($"Comsumer {name} has stop.");
            });
        }
        private volatile bool isWorking = true;
        private Semaphore semamphore;
        private Thread thread = null;

        /// <summary>
        /// Start get resources from share pool.
        /// </summary>
        public void Start()
        {
            // use semaphore to ensure atomic operation.
            semamphore.WaitOne();           
            thread.Start();
            semamphore.Release(1);
        }


        public void Stop()
        {
            isWorking = false;
        }
    }
}
