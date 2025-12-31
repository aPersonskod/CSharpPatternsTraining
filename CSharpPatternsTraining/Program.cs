// See https://aka.ms/new-console-template for more information

using CSharpPatternsTraining.Another;
using CSharpPatternsTraining.Patterns;

var languageFeatures = new LanguageFeatures();

/*var projectRunnner = new ProjectRunner();
projectRunnner.Builder();*/

public class ProjectRunner
{
    public void Factory()
    {
        var factory = new FactoryClass();
        Console.WriteLine(factory.Result);
    }

    public void Observer()
    {
        var observer = new ObserverClass();
    }

    public void DependencyInjection()
    {
        var di = new DependencyInjectionClass();
        di.Run1();
        di.Run2();
    }

    public void Builder()
    {
        var builderClass = new BuilderClass();
    }
}