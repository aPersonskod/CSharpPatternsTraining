using Microsoft.Extensions.DependencyInjection;

namespace CSharpPatternsTraining.Patterns;

public class DependencyInjectionClass
{
    public DependencyInjectionClass()
    {

    }

    public void Run1()
    {
        var logger = new Logger(new DefaultLogService());
        logger.Log("Hello World!");
        logger = new Logger(new GreenLogService());
        logger.Log("Hello Green World!");
    }

    public void Run2()
    {
        var services = new ServiceCollection()
            .AddTransient<ILogService, GreenLogService>()
            .AddTransient<Logger>();
        using var serviceProvider = services.BuildServiceProvider();
        var logService = serviceProvider.GetService<Logger>();
        logService?.Log("Green message");
    }
}

public interface ILogService
{
    void Log(string message);
}

public class DefaultLogService : ILogService
{
    public void Log(string message) => Console.WriteLine(message);
}

public class GreenLogService : ILogService
{
    public void Log(string message)
    {
        var defaultColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ForegroundColor = defaultColor;
    }
}

public class Logger
{
    private ILogService _logService;
    public Logger(ILogService logService) => _logService = logService;
    public void Log(string message) => _logService.Log(message);
}