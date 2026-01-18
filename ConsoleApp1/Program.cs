// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using ConsoleApp1.Opportunities;

Console.WriteLine("Hello, World!");

BenchmarkRunner.Run<UseParallelLinq>();

#region threads testing

/*var threadTesting = new UseThreads();
Console.WriteLine("Use Threads: \n");
threadTesting.UseThreadsTest();
Console.WriteLine("Not locked thread: \n");
threadTesting.UseNotLockedThread();
Console.WriteLine("Locked thread: \n");
threadTesting.UseLockedThread();*/

#endregion
