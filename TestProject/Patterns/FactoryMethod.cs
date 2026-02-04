namespace TestProject.Patterns;

// использовать когда необходимо передать создание обьекта классам наследникам
// (неизвестно заранее какие типы нужно будет создавать)
public class FactoryMethod
{
    [Test]
    public void FactoryMethodTest()
    {
        Creator creator = new WoodCreator("Forest man");
        var house = creator.Create();
        creator = new RockCreator("MMMaster of rock");
        var anotherHouse = creator.Create();
    }
}

internal abstract class Creator(string name)
{
    public string Name { get; set; } = name;
    public abstract House Create();
}

internal class WoodCreator(string name) : Creator(name)
{
    public override House Create() => new WoodenHouse(Name);
}

internal class RockCreator(string name) : Creator(name)
{
    public override House Create() => new RockHouse(Name);
}

internal abstract class House(string masterName)
{
}

internal class WoodenHouse : House
{
    public WoodenHouse(string creatorName) : base(creatorName) => Console.WriteLine($"Wooden House was created by {creatorName}");
}

internal class RockHouse : House
{
    public RockHouse(string creatorName) : base(creatorName) => Console.WriteLine($"Rock House was created by {creatorName}");
}