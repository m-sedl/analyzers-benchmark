using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyThreadPool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyThreadPool.Tests
{
    [TestClass()]
    public class MyTaskTests
    {

        
        [TestMethod()]
        public void MyTaskTest()
        {
            // Test instance of MyTask without CancelSourceToken
            double t() => 10d;
            var pool = new MyTaskPool(10, WorkingModeEnum.Share);
            var task = new MyTask<double>(t, pool, null);
            Assert.AreEqual(false, task.IsCompleted);
            task.Execute();
            Assert.AreEqual(true, task.IsCompleted);
            Assert.AreEqual(10, task.Result);
        }

        [TestMethod()]
        public void MyTaskAggregateExceptionTest()
        {

            double task() => throw new Exception("Test Exception.");
            var pool = new MyTaskPool(10,WorkingModeEnum.Share);
            var t = pool.AddTask<double>(task);
            t.Execute();
            Assert.IsNotNull(t.AggregateException);
        }

        [TestMethod()]
        public void ContinueWithTest()
        {
            double t1() => 1;
            bool t2(double num) => num == 1 ? true : false;
            bool t3(double num) => num == 0 ? true : false;
            var pool = new MyTaskPool(10,WorkingModeEnum.Share);
            var t = pool.AddTask<double>(t1)
                        .ContinueWith<bool>(t2);
            var task2= pool.AddTask<double>(t1)
                        .ContinueWith<bool>(t3);
            t.Execute();
            task2.Execute();

            Assert.AreEqual(true, t.Result);
            Assert.AreEqual(false, task2.Result);

        }

        [TestMethod()]
        public void ExecuteTest()
        {
            var pool = new MyTaskPool(10, WorkingModeEnum.Share);
            var t = pool.AddTask<double>(null);
            t.Execute();
            Assert.AreEqual(true, t.IsCompleted);
            Assert.ThrowsException<AggregateException>(()=>t.Result);
            Assert.IsTrue(t.AggregateException.InnerException is ArgumentNullException);
        }

        
    }
}