namespace TestProject.Opportunities.Model;

public class Account(int sum)
{
    public int Sum { get; private set; } = sum;
    public void Put(int sum) => Sum += sum;
    public void Take(int sum)
    {
        if (Sum >= sum)
        {
            Sum -= sum;
        }
    }
}