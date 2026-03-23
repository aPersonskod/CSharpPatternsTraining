namespace TestProject.Patterns;

public class MediatorTest
{
    [Test]
    public void TestMediator()
    {
        var mediator = new Manager();
        Employee designer = new Designer(mediator);
        Employee programmer = new Programmer(mediator);
        Employee tester = new Tester(mediator);
        mediator.Designer = designer;
        mediator.Programmer = programmer;
        mediator.Tester = tester;
        designer.Send("Захуячил тебе макетики, делай прогу ебатб !!!");
        programmer.Send("Захуячил прогу, проверяй, пидор !!!");
        tester.Send("Че за хуйню ты сделал, давай по новой, все хуйня...");
    }
}

internal abstract class Mediator
{
    public abstract void Send(string message, Employee employee);
}

internal abstract class Employee(Mediator mediator)
{
    public void Send(string message) => mediator.Send(message, this);
    public abstract void Notify(string message);
}

internal class Designer(Mediator mediator) : Employee(mediator)
{
    public override void Notify(string message) => Console.WriteLine($"Сообщение дизайнеру: {message}");
}
internal class Programmer(Mediator mediator) : Employee(mediator)
{
    public override void Notify(string message) => Console.WriteLine($"Сообщение программисту: {message}");
}
internal class Tester(Mediator mediator) : Employee(mediator)
{
    public override void Notify(string message) => Console.WriteLine($"Сообщение тестировщику: {message}");
}
internal class Manager : Mediator
{
    public Employee Designer { get; set; }
    public Employee Programmer { get; set; }
    public Employee Tester { get; set; }

    public override void Send(string message, Employee employee)
    {
        switch (employee)
        {
            case Designer designer:
                Programmer.Notify(message);
                break;
            case Programmer programmer:
                Tester.Notify(message);
                break;
            case Tester tester:
                Designer.Notify(message);
                break;
        }
    }
}