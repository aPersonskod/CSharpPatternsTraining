namespace CSharpPatternsTraining.Patterns;

public class CoContrVariativity
{
    /// <summary>
    /// Covariativity can upcast generic interfaces
    /// </summary>
    public void Covariativity()
    {
        ICovariantGarage<Car> garage = new CovariantGarage<Ford>(new Ford());
        ICovariantGarage<Ford> fordGarage = new CovariantGarage<FordMustang>(new FordMustang());
        fordGarage.SetNumber("1234");
    }

    /// <summary>
    /// Contrvariativity can downcast generic interfaces
    /// </summary>
    public void Contrvariativity()
    {
        IContrvariantGarage<FordMustang> fordGarage = new ContrvariantGarage<Ford>(new Ford());
        fordGarage = new ContrvariantGarage<Car>(new Car());
        fordGarage.SetNumber(new FordMustang());
    }

    public void Invariativity()
    {
        // Garage can be only Car type, 
        IGarage<Car> garage = new Garage<Car>(new Ford());
        IGarage<Ford> fordGarage = new Garage<Ford>((new Car() as Ford)!);
    }
}

internal class Car { }
internal class Ford : Car { }
internal class FordMustang : Ford { }

internal interface IGarage<T>
{
    T CarPlace { get; set; }
}

internal interface ICovariantGarage<out T>
{
    T CarPlace { get; }
    public T SetNumber(string number);
}

internal interface IContrvariantGarage<in T>
{
    T CarPlace { set; }
    public void SetNumber(T carPlace);
}


internal class Garage<T> : IGarage<T>
{
    private T _carPlace;

    public Garage(T carPlace)
    {
        _carPlace = carPlace;
    }

    public T CarPlace
    {
        get => _carPlace;
        set => _carPlace = value;
    }
}

internal class CovariantGarage<T> : ICovariantGarage<T>
{
    private readonly T _carPlace;

    public CovariantGarage(T carPlace)
    {
        _carPlace = carPlace;
    }
    public T CarPlace => _carPlace;
    public T SetNumber(string name) => _carPlace;
}

internal class ContrvariantGarage<T> : IContrvariantGarage<T>
{
    private T _carPlace;

    public ContrvariantGarage(T carPlace)
    {
        _carPlace = carPlace;
    }
    public T CarPlace { set => _carPlace = value; }
    public void SetNumber(T carPlace) => CarPlace = carPlace;
}
