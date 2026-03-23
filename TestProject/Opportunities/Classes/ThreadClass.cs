namespace TestProject.Opportunities.Classes;

public class ThreadClass
{
    [Test]
    public void ThreadClassTest()
    {
        ThreadTestTask();
    }

    private async void ThreadTestTask()
    {
        var mainThread = Thread.CurrentThread;
        mainThread.Name = "Main Thread";
        ThreadInfo(mainThread);
        Console.WriteLine("");
        
        /*var task = DoWork();
        task.Wait();*/
        await DoWork().ConfigureAwait(false);
        
        ThreadInfo(Thread.CurrentThread);
        
        Console.WriteLine($"{mainThread.Name} was exiting");
    }

    private void ThreadInfo(Thread thread)
    {
        var threadName = thread.Name;
        var threadId = thread.ManagedThreadId;
        Console.WriteLine($"{threadName}, Id: {threadId}");
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