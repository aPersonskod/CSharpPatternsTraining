namespace TestProject.Opportunities.Classes;

public class ClosureOpportunityTest
{
    [Test]
    public void Closure()
    {
        var outer = Outer();
        
        outer.Invoke();
        outer.Invoke();
        outer.Invoke();
    }

    private Action Outer()
    {
        var x = 5;

        void Inner()
        {
            Console.WriteLine(++x);
        }

        return Inner;
    }
}