namespace TestProject.Patterns;

public class FactoryTest
{
    public string Result { get; private set; } = "";

    [Test]
    public void TestFactory()
    {
        var countrysideHero = new Hero(new CountrysideManFactory());
        Result += countrysideHero.Name;
        Result += countrysideHero.Run();
        Result += countrysideHero.Hit();
        var bydloHero = new Hero(new BidloManFactory());
        Result += bydloHero.Name;
        Result += bydloHero.Run();
        Result += bydloHero.Hit();
        var mysticHero = new Hero(new MystikManFactory());
        Result += mysticHero.Name;
        Result += mysticHero.Run();
        Result += mysticHero.Hit();
    }
}

public class Hero
{
    private readonly HeroFactory _factory;
    public Hero(HeroFactory factory)
    {
        _factory = factory;
    }

    public string Name => _factory.HeroName + "\r\n";
    public string Run() => _factory.HeroFootWear.Run() + "\r\n";
    public string Hit() => _factory.HeroWeapon.Hit() + "\r\n" + "\r\n";
}

public abstract class FootWear
{
    public abstract string Run();
}

public class Shoes : FootWear
{
    public override string Run() => "Медленно бежит в туфлях";
}

public class Galoshi : FootWear
{
    public override string Run() => "Быстро бежит, но выглядит как мудак";
}

public class Crosses : FootWear
{
    public override string Run() => "Быстро бежит, спортсмен!";
}

public abstract class Weapon
{
    public abstract string Hit();
}

public class Arbalet : Weapon
{
    public override string Hit() => "Выстрел с арбалета";
}

public class Knife : Weapon
{
    public override string Hit() => "Удар ножом";
}

public class FireGun : Weapon
{
    public override string Hit() => "Сжег нахуй всех";
}

public abstract class HeroFactory
{
    public abstract string HeroName { get; }
    public abstract FootWear HeroFootWear { get; }
    public abstract Weapon HeroWeapon { get; }
}

public class CountrysideManFactory : HeroFactory
{
    public override string HeroName { get; } = "Сельский лох =>";
    public override FootWear HeroFootWear { get; } = new Galoshi();
    public override Weapon HeroWeapon { get; } = new Arbalet();
}

public class BidloManFactory : HeroFactory
{
    public override string HeroName { get; } = "Быдло с семками =>";
    public override FootWear HeroFootWear { get; } = new Crosses();
    public override Weapon HeroWeapon { get; } = new Knife();
}

public class MystikManFactory : HeroFactory
{
    public override string HeroName { get; } = "Мистический чел =>";
    public override FootWear HeroFootWear { get; } = new Shoes();
    public override Weapon HeroWeapon { get; } = new FireGun();
}