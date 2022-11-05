using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyThreadPool.Tests
{
    [TestClass]
    public class MainTests
    {
        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void SimpleTestShouldWork(bool isStealing)
        {
            var pool = new CustomThreading.MyThreadPool(5, isStealing);
            var task = pool.Enqueue(() => 5);

            Assert.AreEqual(5, task.Result);

            pool.Dispose();
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void ShouldWorkCorrectlyWhenTaskCountMoreThanThreadCount(bool isStealing)
        {
            var pool = new CustomThreading.MyThreadPool(3, isStealing);
            var tasks = new CustomThreading.IMyTask<int>[10];
            for (int i = 0; i < 10; ++i)
            {
                var localI = i;
                tasks[i] = pool.Enqueue(() =>
                {
                    System.Threading.Thread.Sleep(100);
                    return localI;
                });
            }

            for (int i = 0; i < 10; ++i)
            {
                Assert.AreEqual(i, tasks[i].Result);
            }
            
            pool.Dispose();
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void PoolShutdownShouldNotAffectPreviousTasks(bool isStealing)
        {
            var pool = new CustomThreading.MyThreadPool(10, isStealing);

            var tasks = new CustomThreading.IMyTask<int>[10];
            for (int i = 0; i < 10; ++i)
            {
                var localI = i;
                tasks[i] = pool.Enqueue(() =>
                {
                    System.Threading.Thread.Sleep(50);
                    return localI;
                });
            }
            System.Threading.Thread.Sleep(50);

            for (int i = 0; i < 10; ++i)
            {
                Assert.AreEqual(i, tasks[i].Result);
            }

            pool.Dispose();
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void PoolShouldNotAcceptNewTasksAfterShutdown(bool isStealing)
        {
            var pool = new CustomThreading.MyThreadPool(10, isStealing);
            pool.Enqueue(() => 5);

            pool.Dispose();

            for (int i = 0; i < 10; ++i)
            {
                Assert.AreEqual(null, pool.Enqueue(() => 10));
            }
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void AvailableThreadCountShouldBeEqualToMaxThreadCountInEmptyPool(bool isStealing)
        {
            var pool = new CustomThreading.MyThreadPool(10, isStealing);
            Assert.AreEqual(pool.MaxThreadCount, pool.AvailableThreadCount);
            
            pool.Dispose();
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void AvailableThreadCounterShouldWorkCorrectlyInNonEmptyPool(bool isStealing)
        {
            var pool = new CustomThreading.MyThreadPool(10, isStealing);
            var task = pool.Enqueue(() =>
            {
                System.Threading.Thread.Sleep(50);
                return 5;
            });

            Assert.AreEqual(pool.MaxThreadCount, pool.AvailableThreadCount);

            pool.Dispose();
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void AvailableThreadCountShouldBeMaximalWhenPoolIsFullyBusy(bool isStealing)
        {
            var pool = new CustomThreading.MyThreadPool(5, isStealing);

            for (int i = 0; i < 10; ++i)
            {
                pool.Enqueue(() =>
                {
                    System.Threading.Thread.Sleep(50);
                    return 5;
                });
            }

            Assert.AreEqual(pool.MaxThreadCount, pool.AvailableThreadCount);

            pool.Dispose();
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void ContinueWithShouldWorkCorrectlyOnSimpleTest(bool isStealing)
        {
            var pool = new CustomThreading.MyThreadPool(5, isStealing);
            var task = pool.Enqueue(() => 5);

            var t = task.Result;

            var nestedTask = task.ContinueWith(five => five + 5);
            Assert.AreEqual(10, nestedTask.Result);
            Assert.AreEqual(5, task.Result, "Result of main task was corrupted!");

            pool.Dispose();
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void PoolShouldProceedSeveralNestedTasksFromOneTaskCorrectly(bool isStealing)
        {
            var pool = new CustomThreading.MyThreadPool(5, isStealing);
            var mainTask = pool.Enqueue(() => 5);

            var nestedTasks = new CustomThreading.IMyTask<int>[10];
            for (int i = 0; i < 10; ++i)
            {
                var localI = i;
                nestedTasks[i] = mainTask.ContinueWith(mainValue => mainValue + localI);
            }

            for (int i = 0; i < 10; ++i)
            {
                Assert.AreEqual(5 + i, nestedTasks[i].Result);
            }

            pool.Dispose();
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void NestedTaskShouldWorkCorrectlyWithLongMainTask(bool isStealing)
        {
            var pool = new CustomThreading.MyThreadPool(5, isStealing);

            var mainTask = pool.Enqueue(() =>
            {
                System.Threading.Thread.Sleep(100);
                return 10;
            });
            var nestedTask = mainTask.ContinueWith(mainValue => mainValue + 15);

            Assert.AreEqual(25, nestedTask.Result);

            pool.Dispose();
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void NestedTaskShouldWorkCorrectlyWhenMainTaskIsOutOfView(bool isStealing)
        {
            var pool = new CustomThreading.MyThreadPool(5, isStealing);

            var mainTask = pool.Enqueue(() =>
            {
                System.Threading.Thread.Sleep(100);
                return 10;
            });
            var nestedTask = mainTask.ContinueWith(mainValue => mainValue + 15);

            mainTask = null;

            Assert.AreEqual(25, nestedTask.Result);

            pool.Dispose();
        }
    }
}
