namespace Program
{
    using System;

    public static class Program
    {
        public static void Main(string[] args)
        {
            const int taskCount = 10;
            const int availableThreadsInPool = 3;

            var pool = new CustomThreading.MyThreadPool(availableThreadsInPool, false);

            var tasks = new CustomThreading.IMyTask<int>[taskCount];
            for (int i = 0; i < tasks.Length; ++i)
            {
                tasks[i] = pool.Enqueue(TaskReturn15);
            }

            Console.WriteLine("There should be 15 (x{0}):", taskCount);
            foreach (var task in tasks)
            {
                Console.WriteLine(task.Result);
            }

            var nestedTasks = new CustomThreading.IMyTask<int>[taskCount];
            for (int i = 0; i < tasks.Length; ++i)
            {
                nestedTasks[i] = tasks[i].ContinueWith(TaskReturnOldPlus38);
            }

            Console.WriteLine("There should be 53 (x{0}):", taskCount);
            foreach (var task in nestedTasks)
            {
                Console.WriteLine(task.Result);
            }

            Console.WriteLine("And there should be 15 (x{0}) again:", taskCount);
            foreach (var task in tasks)
            {
                Console.WriteLine(task.Result);
            }
        }

        public static int TaskReturn15()
        {
            return 15;
        }

        public static int TaskReturnOldPlus38(int oldValue)
        {
            return oldValue + 38;
        }
    }
}
