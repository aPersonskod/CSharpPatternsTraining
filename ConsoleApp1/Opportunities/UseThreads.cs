namespace ConsoleApp1.Opportunities;

public class UseThreads
{
    public void UseThreadsTest()
    {
        Thread th1 = new Thread(AnotherThread);
        th1.Name = "Thread 1";
        th1.Start();

        for (int i = 0; i < 7; i++)
        {
            Console.WriteLine($"Main Thread: {i}");
            Thread.Sleep(300);
        }
    }

    public void UseNotLockedThread()
    {
        int x = 0;

        for (int i = 0; i < 3; i++)
        {
            Thread th = new Thread(Print);
            th.Name = "Thread " + i;
            th.Start();
        }

        Console.WriteLine("Done !!!");

        void Print()
        {
            x = 1;
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
                x++;
                Thread.Sleep(100);
            }
        }
    }

    public void UseLockedThread()
    {
        int x = 0;
        object locker = new object();

        for (int i = 0; i < 3; i++)
        {
            Thread th = new Thread(Print);
            th.Name = "Thread " + i;
            th.Start();
        }
        
        Console.WriteLine("Done !!!");
        
        void Print()
        {
            lock (locker)
            {
                x = 1;
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
                    x++;
                    Thread.Sleep(100);
                }
            }
        }
    }

    private void AnotherThread()
    {
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"{Thread.CurrentThread.Name}: {i}");
            Thread.Sleep(400);
        }
    }
}

public class TestThreadCalls
{
    public void ThreadClassTest()
    {
        var task = ThreadTestTask();
        task.Wait();
    }

    private async Task ThreadTestTask()
    {
        var mainThread = Thread.CurrentThread;
        mainThread.Name = "Main Thread";
        ThreadInfo(mainThread);
        Console.WriteLine("");
        
        /*var task = DoWork();
        task.Wait();*/
        await DoWork();
        
        ThreadInfo(Thread.CurrentThread, "was exiting");
    }

    private void ThreadInfo(Thread thread, string? addition = null)
    {
        var threadName = thread.Name;
        var threadId = thread.ManagedThreadId;
        var result = $"{threadName}, Id: {threadId}";
        if (addition != null) result += $", {addition}";
        Console.WriteLine(result);
    }

    private async Task DoWork()
    {
        Console.WriteLine("1");
        var thread1 = Thread.CurrentThread;
        ThreadInfo(thread1);
        Console.WriteLine("");
        
        await Task.Delay(1000);
        Console.WriteLine("After waiting: ");
        var thread2 = Thread.CurrentThread;
        thread2.Name = "Second Thread";
        ThreadInfo(thread2);
        Console.WriteLine("");
        
        Console.WriteLine("2");
        ThreadInfo(Thread.CurrentThread);
        Console.WriteLine("");
    }
}