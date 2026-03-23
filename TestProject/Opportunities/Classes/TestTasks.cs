namespace TestProject.Opportunities.Classes;

public class TestTasks
{
    [Test]
    public void TestTask()
    {
        var action = () =>
        {
            Thread.Sleep(2000);
            Console.WriteLine("This is a task");
        };
        var simpleTask = new Task(action);

        simpleTask.Start();
        simpleTask.Wait();
        
        new Task(action).RunSynchronously();
    }

    [Test]
    public void TestNestedTask()
    {
        var action = () =>
        {
            Thread.Sleep(2000);
            Console.WriteLine("This is a task");
        };
        
        var nestedTask = new Task(() =>
        {
            action();
            new Task(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("Task in Task");
            }, TaskCreationOptions.AttachedToParent).Start();
        });
        
        nestedTask.Start();
        nestedTask.Wait();
        
        Console.WriteLine("End of method");
    }

    [Test]
    public void TestResultTask()
    {
        int num1 = 3, num2 = 5;
        var task = new Task<int>(() => Action(num1, num2));
        task.Start();
        var result = task.Result;
        Console.WriteLine(result);
        Assert.That(result, Is.EqualTo(8));
        
        return;
        int Action(int a, int b) => a + b;
    }

    [Test]
    public void CancelTask1()
    {
        using var cts = new CancellationTokenSource();
        var token = cts.Token;
        
        var task = new Task(() =>
        {
            for (int i = 0; i < 100; i++)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }

                Console.WriteLine(i);
                Thread.Sleep(200);
            }
        }, token);
        task.Start();
        
        Thread.Sleep(2000);
        cts.Cancel();
        task.Wait();
    }
    
    [Test]
    public void CancelTask2()
    {
        using var cts = new CancellationTokenSource();
        var token = cts.Token;
        
        var task = new Task(() =>
        {
            for (int i = 0; i < 100; i++)
            {
                if (token.IsCancellationRequested)
                {
                    token.ThrowIfCancellationRequested();
                }

                Console.WriteLine(i);
                Thread.Sleep(200);
            }
        }, token);
        try
        {
            task.Start();
        
            Thread.Sleep(2000);
            cts.Cancel();
            task.Wait();
        }
        catch (AggregateException ae)
        {
            foreach (var e in ae.InnerExceptions)
            {
                Console.WriteLine(e is OperationCanceledException
                    ? "Operation was canceled"
                    : e.Message);
            }
        }
    }
}