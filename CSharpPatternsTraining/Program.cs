// See https://aka.ms/new-console-template for more information

using CSharpPatternsTraining.Patterns;

var factory = new FactoryClass();
//Console.WriteLine(factory.Result);
//var observer = new ObserverClass();
var di = new DependencyInjectionClass();
di.Run1();
di.Run2();