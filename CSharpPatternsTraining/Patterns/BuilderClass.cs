namespace CSharpPatternsTraining.Patterns;

public class BuilderClass
{
    public BuilderClass()
    {
        var director = new TvDirector();
        var saturnTv = director.CreateTv(new SaturnTvBuilder());
        saturnTv.GetDescription();
        var samsungDirector = director.CreateTv(new SamsungTvBuilder());
        samsungDirector.GetDescription();
    }
}

internal class TvDirector
{
    public Tv CreateTv(TvBuilder builder)
    {
        builder.SetBrand();
        builder.SetPrice();
        builder.SetSize();
        return builder.CreatedTv;
    }
}

internal abstract class TvBuilder
{
    public Tv CreatedTv { get; } = new Tv();
    public abstract void SetPrice();
    public abstract void SetSize();
    public abstract void SetBrand();
    
}

internal class SamsungTvBuilder : TvBuilder
{
    public override void SetPrice() => CreatedTv.Price = 30000;

    public override void SetSize() => CreatedTv.Size = 34;

    public override void SetBrand() => CreatedTv.Brand = "Samsung";
}

internal class SaturnTvBuilder : TvBuilder
{
    public override void SetPrice() => CreatedTv.Price = 10000;

    public override void SetSize() => CreatedTv.Size = 17;

    public override void SetBrand() => CreatedTv.Brand = "Saturn";
}

internal class Tv
{
    public int Price { get; set; }
    public int Size { get; set; }
    public string Brand { get; set; }

    public void GetDescription()
    {
        Console.WriteLine($"You have {Brand} television with size {Size} that cost {Price}.");
    }
}