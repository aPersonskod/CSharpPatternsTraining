using CSharpPatternsTraining.Another.Opportunities;

namespace CSharpPatternsTraining.Another;

public class LanguageFeatures
{
    public LanguageFeatures()
    {
        RunDelegateTraining();
        //RunEventTraining();
    }
    
    public void CoContrVariativity()
    {
        var variativity = new CoContrVariativity();
        variativity.Covariativity();
        variativity.Contrvariativity();
        variativity.Invariativity();
    }
    
    
    public void CoContrVariativityDelegates()
    {
        var variativity = new CoContrVariativityDelegate();
        variativity.CoVariativity();
        variativity.ContrVariativity();
    }
    
    private void RunDelegateTraining()
    {
        var delegateTraining = new DelegateTraining();
        
        Console.WriteLine("This is running delegate:");
        delegateTraining.RunDelegate(DelegateTraining.ConsoleMessage);
        Console.WriteLine("");
        
        Console.WriteLine("This is multiple actions in one delegate:");
        Message multipleActions = DelegateTraining.ConsoleMessage;
        multipleActions += DelegateTraining.RedConsoleMessage;
        delegateTraining.RunDelegate(multipleActions);
        Console.WriteLine("");
        
        Console.WriteLine("This is running null delegate:");
        delegateTraining.RunDelegateWithNull(null);
        Console.WriteLine("");
        
        delegateTraining.UseAction(DelegateTraining.Sum);
        Console.WriteLine("");
        
        delegateTraining.UsePredicate(DelegateTraining.IsPositive);
        Console.WriteLine("");
        
        delegateTraining.UseFunc(DelegateTraining.Square);
        Console.WriteLine("");
    }

    private void RunEventTraining()
    {
        var eventTraining = new EventTraining();
        
        Console.WriteLine("This is event using for notifying:\n");
        eventTraining.NotifyEvent += DelegateTraining.RedConsoleMessage;
        eventTraining.WriteMessage();
        Console.WriteLine("");
        
        Console.WriteLine("This is event using for notifying with event args:\n");
        eventTraining.Notify += NotifyEventHandler;
        eventTraining.RunWithEventArgs();
        Console.WriteLine("");
    }

    private void NotifyEventHandler(EventTraining sender, MessageEventArgs e)
    {
        Console.WriteLine($"Message: {e.Message}");
        Console.WriteLine($"Author: {e.Author}");
        Console.WriteLine($"Message successfuly was sent in {sender.Messenger}");
    }
}