using System.Runtime.Versioning;
using BenchmarkDotNet.Attributes;

namespace TestProject.Opportunities.Classes;

public class LinqTraining
{
    // find words with starts on 't'
    [Test]
    public void FindTWords()
    {
        string[] people = ["Tom", "Bob", "Sam", "Tim", "Tomas", "Bill"];
        var tPeople = people.Where(x => x.ToUpper().StartsWith('T')).OrderBy(x => x);
        foreach (var tPerson in tPeople) Console.WriteLine(tPerson);
    }

    [Test]
    public void UnionCollections()
    {
        var companies = new List<Company>()
        {
            new Company("Amazon", [
                new Person("Tom", "Holland"),
                new Person("Petya", "Patochkin"),
                new Person("Jason", "Statham")
            ]),
            new Company("Samsung", [
                new Person("Loh", "Flowered"),
                new Person("Jackie", "Chan"),
                new Person("Hyjlo", "Zamorskoe")
            ]),
        };

        var list = companies.SelectMany(x => x.People);
        foreach (var l in list) Console.WriteLine($"{l.FirstName} {l.LastName}");
        var list2 = companies.SelectMany(x => x.People, (x, y) => new { x.Name, y.FirstName, y.LastName });
        foreach (var l2 in list2) Console.WriteLine($"{l2.Name} {l2.FirstName} {l2.LastName}");
    }

    private readonly IEnumerable<int> _numbers = Enumerable.Range(1, 200);

    [Test]
    public void TestSquareParallelLinq()
    {
        _numbers.AsParallel().Select(x => x * x).ForAll(Console.WriteLine);
    }
    
    [Test]
    public void TestSquareLinq()
    {
        var squares = _numbers.Select(x => x * x);
        foreach (var square in squares) Console.WriteLine(square);
    }
}

internal record Person(string FirstName, string LastName);
internal record Company(string Name, List<Person> People);