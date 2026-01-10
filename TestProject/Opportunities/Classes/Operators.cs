namespace TestProject.Opportunities.Classes;

public class Operators
{
    [Test]
    public void UseOverrideOperator()
    {
        Console.WriteLine("Use Override Operator + for 2 objects");
        var product1 = new Product() { Name = "Potatoes", Cost = 20 };
        var product2 = new Product() { Name = "Washed potatoes", Cost = 30 };
        var product3 = product1 + product2;
        Console.WriteLine($"Final product: Name = {product3.Name}, Cost = {product3.Cost}");
    }
}

public class Indexators
{
    public Indexators()
    {
        var user = new User();
        user["name"] = "Petya";
        user["email"] = "petya@gmail.com";
        Console.WriteLine(user["name"]);
    }

    class User
    {
        private string name;
        private string email;

        public string this[string index]
        {
            get => index switch
            {
                "name" => name,
                "email" => email,
                _ => throw new ArgumentOutOfRangeException(nameof(index), index, null)
            };
            set
            {
                switch (index)
                {
                    case "name":
                        name = value;
                        break;
                    case "email":
                        email = value;
                        break;
                }
            }
        }
    }
}

internal class Product
{
    public string Name { get; set; }
    public int Cost { get; set; }

    public static Product operator +(Product p1, Product p2)
    {
        return new Product() { Name = p2.Name, Cost = p1.Cost + p2.Cost };
    }
}