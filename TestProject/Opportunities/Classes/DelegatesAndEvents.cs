using TestProject.Opportunities.Model;

namespace TestProject.Opportunities.Classes;

public delegate void Message(string message);

public class DelegateTraining
{
    public void RunDelegate(Message message)
    {
        message("This is working delegate");
    }

    public void RunDelegateWithNull(Message? message)
    {
        // need to use like here
        message?.Invoke("Delegate has no realization");
    }

    public void UseAction(Action<int, int> action)
    {
        Console.WriteLine($"Action has contr-variant type");
        action.Invoke(3, 7);
    }

    public void UsePredicate(Predicate<int> predicate)
    {
        var num = 5;
        Console.WriteLine($"Predicate has contr-variant type and bool return type");
        Console.WriteLine(predicate.Invoke(num) ? $"{num} is positive" : $"{num} is negative");
    }

    public void UseFunc(Func<int, int> func)
    {
        Console.WriteLine($"Action has contr-variant type and co-variant return type");
        Console.WriteLine($"Result: {func.Invoke(3)}");
    }

    public static void ConsoleMessage(string message) => Console.WriteLine(message);
    public static void RedConsoleMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    public static int Square(int number) => number * number;
    public static int Sum(int number) => number + number;
    public static void Sum(int n1, int n2) => ConsoleMessage($"Result: {n1 + n2}");
    public static bool IsPositive(int number) => number > 0;
}

public class EventTraining
{
    public delegate void MessageWriter(EventTraining sender, MessageEventArgs e);
    public event MessageWriter? Notify;
    public event Message? NotifyEvent;

    public string Messenger { get; private set; }
    
    public void WriteMessage()
    {
        Console.WriteLine("I've just write message !!!");
        NotifyEvent?.Invoke("Message was sent");
    }

    public void RunWithEventArgs()
    {
        Messenger = "Telegram";
        Console.WriteLine("I've just write message !!!");
        Notify?.Invoke(this, new MessageEventArgs("Message was sent", "Important man"));
    }
}

public class MessageEventArgs(string message, string author)
{
    public string Message => message;
    public string Author => author;
}

public delegate Car CarBuilder();
public delegate void FordBuilder(Ford car);

public class CoContrVariativityDelegate
{
    public void CoVariativity()
    {
        CarBuilder carBuilder = () => new Ford(); // ковариантность - универсальный тип принимает конкретный тип
        carBuilder.Invoke();
    }

    public void ContrVariativity()
    {
        FordBuilder fordBuilder = (car) => new Car(); // контрвариантность - конкретный тип принимает более универсальный тип
        fordBuilder.Invoke(new Ford());
    }
}