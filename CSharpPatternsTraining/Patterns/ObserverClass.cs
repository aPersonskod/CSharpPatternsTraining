namespace CSharpPatternsTraining.Patterns;

public class ObserverClass
{
    public ObserverClass()
    {
        var tgChanel = new TgChanel();
        var user1 = new User("Петя", tgChanel);
        var user2 = new User("Вася", tgChanel);
        var user3 = new User("Жора", tgChanel);
        var user4 = new User("Галя", tgChanel);
        
        tgChanel.WriteArticle("Важный хуй создал канал");
        tgChanel.WriteArticle("Важный хуй: Нахуй Васю !!!");
        tgChanel.RemoveObserver(user2);
        tgChanel.WriteArticle("Важный хуй: Для всех остальных, здарова)");
        
    }
}

public interface IObservable
{
    void AddObserver(IObserver observer);
    void RemoveObserver(IObserver observer);
    void NotifyObservers();
}

public class TgChanel : IObservable
{
    private List<IObserver> _users { get; } = new List<IObserver>();
    private UserMessage _userMessage;

    public void AddObserver(IObserver observer)
    {
        _users.Add(observer);
        _userMessage = new UserMessage("");
    }

    public void RemoveObserver(IObserver observer)
    {
        _users.Remove(observer);
        Console.WriteLine($"{observer.Name} был удален из чата");
    }

    public void NotifyObservers()
    {
        foreach (var observer in _users) observer.Update(_userMessage);
    }

    public void WriteArticle(string article)
    {
        _userMessage = new UserMessage(article);
        NotifyObservers();
    }
}

public interface IObserver
{
    string Name { get; }
    void Update(UserMessage info);
    void StopUpdate();
}

public class User : IObserver
{
    public string Name { get; private set; }
    private readonly IObservable _observable;

    public User(string name, IObservable observable)
    {
        Name = name;
        _observable = observable;
        _observable.AddObserver(this);
    }

    public void Update(UserMessage info)
    {
        Console.WriteLine($"{Name} получил сообщение : {info.Message}");
    }

    public void StopUpdate()
    {
        _observable.RemoveObserver(this);
        Console.WriteLine($"{Name} был удален из чата");
    }
}

public class UserMessage
{
    public string Message { get; set; }
    public UserMessage(string message)
    {
        Message = message;
    }

}