using System.Collections;

namespace TestProject.Opportunities.Classes;

public class CollectionsOpportunities
{
    [Test]
    public void IEnumerableTest()
    {
        var days = new Week();
        foreach (var day in days)
        {
            Console.WriteLine(day);
        }
    }

    [Test]
    public void IteratorTest()
    {
        foreach (var number in GetNumbers()) Console.WriteLine(number);
    }

    private IEnumerable<int> GetNumbers()
    {
        for (var i = 0; i < 10; i++)
        {
            if (i is 7) yield break;
            yield return i * i;
        }
    }
}

public class Week : IEnumerable
{
    private string[] _weeks = ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];
    public IEnumerator GetEnumerator() => _weeks.GetEnumerator();
}