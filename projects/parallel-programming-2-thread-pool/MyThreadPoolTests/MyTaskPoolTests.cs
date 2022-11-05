using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyThreadPool;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyThreadPool.Tests
{
    [TestClass()]
    public class MyTaskPoolTests
    {
        [TestMethod()]
        public void MyTaskPoolTest()
        {
            // Test creating pool with num.
            var pool = new MyTaskPool(10, WorkingModeEnum.Share);
            Assert.AreEqual(10,pool.ThreadCount);

            pool = new MyTaskPool(5, WorkingModeEnum.Stealing);
            Assert.AreEqual(5, pool.ThreadCount);
        }


        [TestMethod()]
        public void AddTaskTest()
        {
            var pool = new MyTaskPool(10,WorkingModeEnum.Share);
            double task() => 10d;

            for(int i = 0; i < 5; i++)
            {
                var t = pool.AddTask(task);
                Thread.Sleep(200);
                Assert.AreEqual(10, t.Result);
            }

            var list = new List<IMyTask<double>>();
            pool = new MyTaskPool(10, WorkingModeEnum.Stealing);
            for (int i = 0; i < 5; i++)
            {
                var t = pool.AddTask(task);
                list.Add(t);
                Thread.Sleep(500);
            }

            for (int i = 0; i < 5; i++)
                Assert.AreEqual(10, list[i].Result);
        }
    
        [TestMethod()]
        public void ShutDownTest()
        {
            var pool = new MyTaskPool(10, WorkingModeEnum.Share);
            double task() => 10d;
            var t = pool.AddTask<double>(task);
            Thread.Sleep(1000);
            pool.ShutDown();
            var t1 = pool.AddTask<double>(task);
            Assert.AreEqual(true, t.IsCompleted);
            Assert.AreEqual(null, t1);
        }
    
        [TestMethod()]
        public void DisposeTest()
        {
            var pool = new MyTaskPool(10, WorkingModeEnum.Share);
            double task() => 10d;
            var t = pool.AddTask<double>(task);
            Thread.Sleep(200);
            Assert.AreEqual(true, t.IsCompleted);
            pool.Dispose();
            Assert.AreEqual(0, pool.ThreadCount);
        }
    
    
        [TestMethod()]
        public void LargeTasksTest()
        {
            double t1() => 1;
            bool t2(double num) => num == 1 ? true : false;
            string t3(bool result) => result ? "True" : "False";
            var pool = new MyTaskPool(20, WorkingModeEnum.Share);
    
            var tasks = new List<MyTask<double>>(20);
            for(int i = 0; i < 20; i++)
            {
                tasks.Add((MyTask<double>)pool.AddTask<double>(t1));
            }
    
            for(int i = 0; i <20; i++)
            {
                Assert.AreEqual(1, tasks[i].Result);
            }
            pool.Dispose();
        }


        [TestMethod]
        public void ContinueWithMultiTasksTest()
        {
            double t1() => 1;
            bool t2(double num) => num == 1;
            string t3(bool result) => result ? "True" : "False";
            var pool = new MyTaskPool(15, WorkingModeEnum.Stealing);
            var task = pool.AddTask<double>(t1)
                            .ContinueWith<bool>(t2)
                            .ContinueWith<string>(t3);
            Assert.AreEqual("True", task.Result);
    
            var tasks1 = new BlockingCollection<MyTask<string>>(15);
            for (int i = 0; i <15; i++)
            {
                tasks1.Add((MyTask<string>)pool.AddTask<double>(t1)
                                            .ContinueWith<bool>(t2)
                                            .ContinueWith<string>(t3));
                Thread.Sleep(500);
            }
            foreach(var t in tasks1)
            {
                Assert.AreEqual("True", t.Result);
            }
        }
    }
}