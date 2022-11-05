using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSynchronization
{
    /// <summary>
    /// Create consumers and producers,
    /// consumer receive messages from producer
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {

            int numOfProducers = 0, numOfConsumers = 0;
            Console.Write("Please enter the number of Producers: ");
            if(int.TryParse(Console.ReadLine(), out numOfProducers))
            {
                if(numOfProducers <= 0)
                {
                    Console.WriteLine("The number of producers must be greater than 0.");
                    Console.ReadKey();
                    return;
                }
            }
            else
            {
                Console.WriteLine("Error params.");
                Console.ReadKey();
                return;
            }
            Console.Write("Please enter the number of Consumers: ");
            if (int.TryParse(Console.ReadLine(), out numOfConsumers))
            {
                if (numOfProducers <= 0)
                {
                    Console.WriteLine("The number of consumers must be greater than 0.");
                    Console.ReadKey();
                    return;
                }
            }
            else
            {
                Console.WriteLine("Error params.");
                Console.ReadKey();
                return;
            }


            MessageQueue queue = new MessageQueue();
            List<Producer> producers = new List<Producer>(4);
            List<Consumer> consumers = new List<Consumer>(4);
            for(int i = 0; i < numOfProducers; i++)
            {
                producers.Add(new Producer((i + 1).ToString(), queue));
                
            }
            for(int i=0;i< numOfConsumers; i++)
            {
                consumers.Add(new Consumer((i + 1).ToString(), queue));
            }

            

            foreach (var p in producers)
                p.Start();
            foreach (var c in consumers)
                c.Start();

            Console.ReadKey();
            Console.WriteLine("Stop threads......");
            for(int i = 0; i < numOfProducers; i++)
            {
                producers[i].Stop();
                
            }

            for(int i = 0; i < numOfConsumers; i++)
            {
                consumers[i].Stop();
            }          
            Console.ReadKey();
        }
    }
}
