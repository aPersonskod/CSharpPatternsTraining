namespace TestProject.Patterns;

// используется когда динамически нужно добавлять функциональность
public class Decorator
{
    [Test]
    public void TestDecorator()
    {
        Pizza pizza = new ItalianPizza("Итальянская пицца");
        WriteInfo(pizza);
        Assert.That(pizza.GetCost(), Is.EqualTo(400));
        pizza = new CheesePizza(pizza);
        WriteInfo(pizza);
        Assert.That(pizza.GetCost(), Is.EqualTo(430));
        pizza = new TomatoPizza(pizza);
        WriteInfo(pizza);
        Assert.That(pizza.GetCost(), Is.EqualTo(450));
    }

    [Test]
    public void TestDecoratorWithExt()
    {
        Pizza pizza = new ItalianPizza("Итальянская пицца");
        WriteInfo(pizza);
        Assert.That(pizza.GetCost(), Is.EqualTo(400));
        pizza = pizza.Decorate(p => new CheesePizza(p));
        WriteInfo(pizza);
        Assert.That(pizza.GetCost(), Is.EqualTo(430));
        pizza = pizza.Decorate(p => new TomatoPizza(p));
        WriteInfo(pizza);
        Assert.That(pizza.GetCost(), Is.EqualTo(450));
    }

    private void WriteInfo(Pizza pizza)
    {
        var info = $"Сделана {pizza.Name}\n";
        info += $"Цена: {pizza.GetCost()}";
        Console.WriteLine(info);
    }
}

internal abstract class Pizza(string name)
{
    public string Name { get; } = name;
    public abstract int GetCost();
}

internal class ItalianPizza(string name) : Pizza(name)
{
    public override int GetCost() => 400;
}

internal abstract class PizzaDecorator(string name, Pizza pizza) : Pizza(name)
{
    private Pizza CreatedPizza { get; } = pizza;
}

internal class TomatoPizza : PizzaDecorator
{
    private readonly Pizza _pizza;

    public TomatoPizza(Pizza pizza) : base(pizza.Name + ", с томатами", pizza)
    {
        _pizza = pizza;
        Console.WriteLine("Add some tomato to pizza...");
    }

    public override int GetCost() => _pizza.GetCost() + 20;
}

internal class CheesePizza : PizzaDecorator
{
    private readonly Pizza _pizza;

    public CheesePizza(Pizza pizza) : base(pizza.Name + ", с сыром", pizza)
    {
        _pizza = pizza;
        Console.WriteLine("Add some cheese to pizza...");
    }

    public override int GetCost() => _pizza.GetCost() + 30;
}

internal static class DecoratorExtensions
{
    public static T Decorate<T>(this T obj, Func<T, T> decorated) => decorated(obj);
}