using System.Diagnostics;
using Xunit.Abstractions;
using XUnitTestProject.Models;
using XUnitTestProject.Services;

namespace XUnitTestProject;

public class TaskTests(ITestOutputHelper testOutputHelper)
{
    private readonly CarCreator _carCreator = new(new Car()
    {
        Name = "Green lamba"
    });

    [Fact]
    public async Task Test_TaskIsCompleted()
    {
        var orderCarTask = _carCreator.OrderCarAsync();
        var car = await orderCarTask;
        Assert.True(car.IsOrdered);
        Assert.True(orderCarTask.IsCompletedSuccessfully);
        
        var buyCarTask = _carCreator.BuyCarAsync(true);
        Assert.False(buyCarTask.IsCompletedSuccessfully);
        car = await buyCarTask;
        Assert.True(car.IsOrdered);
        Assert.True(car.IsBought);
        Assert.True(buyCarTask.IsCompletedSuccessfully);
        
        var deliverCarTask = _carCreator.DeliverCarAsync();
        var message = await deliverCarTask;
        Assert.True(car.IsOrdered);
        Assert.True(car.IsBought);
        Assert.True(deliverCarTask.IsCompletedSuccessfully);
        Assert.Equal($"Your {car.Name} successfully delivered!", message);
    }

    [Fact]
    public async Task Test_TaskIsNotCompleted()
    {
        var buyCarTask = _carCreator.BuyCarAsync(true);
        var ex = await Assert.ThrowsAsync<Exception>(async () => await buyCarTask);
        Assert.Equal("Car is not ordered", ex.Message);
        Assert.False(buyCarTask.IsCompletedSuccessfully);
    }

    [Fact]
    public async Task Test_SagaIsCompleted()
    {
        var car = await _carCreator.OrderCarAsync();
        Assert.True(car.IsOrdered);
        var buyCarTask = _carCreator.BuyCarAsync(false);
        var cancelOrderTask = _carCreator.CancelOrderCarAsync();
        try
        {
            await buyCarTask;
        }
        catch (Exception)
        {
            car = await cancelOrderTask;
        }
        Assert.False(buyCarTask.IsCompletedSuccessfully);
        Assert.True(cancelOrderTask.IsCompletedSuccessfully);
        Assert.False(car.IsOrdered);
    }

    [Fact]
    public async Task Test_CancelOrderCarTask()
    {
        using var cancelOrderSource = new CancellationTokenSource();
        var token = cancelOrderSource.Token;
        
        var orderCarTask = _carCreator.OrderCarAsync(token);

        cancelOrderSource.CancelAfter(TimeSpan.FromSeconds(2));

        await Assert.ThrowsAsync<TaskCanceledException>(() => orderCarTask);
    }

    [Fact]
    public async Task Test_ParallelTaskExecuting()
    {
        var stopwatch = Stopwatch.StartNew();
        
        try
        {
            var task1 = WriteRandomNumber(1);
            var task2 = WriteRandomNumber(2);
            var task3 = ThrowRandomNumber(3);
            
            await Task.WhenAll(task1, task2, task3);
            
            // or
            //await task1; await task2; await task3;
        }
        catch (Exception e)
        {
            testOutputHelper.WriteLine(e.Message);
        }
        
        stopwatch.Stop();
        Assert.True(stopwatch.ElapsedMilliseconds <= 3000);
    }
    
    [Fact]
    public async Task Test_SequentialTaskExecuting()
    {
        var stopwatch = Stopwatch.StartNew();
        
        try
        {
            await WriteRandomNumber(1);
            await WriteRandomNumber(2);
            await ThrowRandomNumber(3);
        }
        catch (Exception e)
        {
            testOutputHelper.WriteLine(e.Message);
        }
        
        stopwatch.Stop();
        Assert.True(stopwatch.ElapsedMilliseconds >= 3000);
    }

    private async Task WriteRandomNumber(int number)
    {
        testOutputHelper.WriteLine($"Creating random number №{number}");
        var random = new Random();
        await Task.Delay(1000);
        testOutputHelper.WriteLine($"Random number: {random.Next(10, 100)}");
    }
    
    private async Task ThrowRandomNumber(int number)
    {
        testOutputHelper.WriteLine($"Trying to get random number №{number}");
        await Task.Delay(1000);
        throw new InvalidOperationException("Unhandled exception");
    }
}