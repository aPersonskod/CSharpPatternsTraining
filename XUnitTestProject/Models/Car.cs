namespace XUnitTestProject.Models;

public class Car
{
    public string Name { get; init; }
    public bool IsOrdered { get; set; }
    public bool IsBought { get; private set; }

    public void BuyCar(bool isBought)
    {
        if (!IsOrdered) throw new Exception("Car is not ordered");
        IsBought = isBought;
    }

    public string DeliverCar()
    {
        if (!IsBought) throw new Exception("Car is not bought");
        return $"Your {Name} successfully delivered!";
    }
}