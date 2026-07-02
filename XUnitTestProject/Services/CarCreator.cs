using XUnitTestProject.Models;

namespace XUnitTestProject.Services;

public class CarCreator(Car car)
{
    public async Task<Car> OrderCarAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        car.IsOrdered = true; 
        await Task.Delay(2200, cancellationToken);
        return car;
    }
    
    public async Task<Car> CancelOrderCarAsync()
    {
        car.IsOrdered = false;
        await Task.Delay(1000);
        return car;
    }
    
    public async Task<Car> BuyCarAsync(bool isBought)
    {
        if (!isBought) throw new Exception($"You can't buy {car.Name}");
        car.BuyCar(isBought);
        await Task.Delay(1000);
        return car;
    }
    
    public async Task<string> DeliverCarAsync()
    {
        var message = car.DeliverCar();
        await Task.Delay(1000);
        return message;
    }
}