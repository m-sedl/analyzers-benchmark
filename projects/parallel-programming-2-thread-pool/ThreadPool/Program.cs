using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace MyThreadPool
{
    class Program
    {
        static void Main(string[] args)
        {
            double t1() => 1;
            double t21() => 2;

            bool t2(double num) => num == 1 ? true : false;
            string t3(bool result) => result ? "True" : "False";
            var pool = new MyTaskPool(5, WorkingModeEnum.Share);
            var tasks1 = new List<MyTask<string>>(100);

          // var t = pool.AddTask<double>(t1)
          //              .ContinueWith<bool>(t2)
          //              .ContinueWith<string>(t3);
          //  Console.WriteLine(t.Result);
         //  var tt = pool.AddTask<double>(t21);
           var list = new BlockingCollection<IMyTask<string>>();
           for (int i = 0; i < 10; i++)
           {
               var tmp = pool.AddTask<double>(t21)
                           .ContinueWith(t2)
                           .ContinueWith(t3);
               list.Add(tmp);
               Thread.Sleep(500);
           }
         
           foreach (var l in list)
               Console.WriteLine(l.Result);
        //  Console.WriteLine(t.Result);
        //  Console.WriteLine(tt.Result);
            Console.ReadKey();
        }

        static double Sum()
        {
            return 1;
        }

        static double Sum(double num)
        {
            return num + 1;
        }

        static void Display()
        {
            Console.WriteLine( "Hello World.");
        }
    }
}
