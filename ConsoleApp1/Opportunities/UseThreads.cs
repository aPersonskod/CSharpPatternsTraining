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