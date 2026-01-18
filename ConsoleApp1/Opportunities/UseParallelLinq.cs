using BenchmarkDotNet.Attributes;

namespace ConsoleApp1.Opportunities;

// Benchmark needs Release configuration !!!
[MemoryDiagnoser]
public class UseParallelLinq
{
    private readonly IEnumerable<int> _numbers = Enumerable.Range(1, 200);
    /*
     На таких примерах паралельность безполезна, необходимы гараздо более серьезные 
     вычисления чтобы паралельность была к месту.
     На данном примере паралельность медленне потому что используется больше ресурсов 
     на использование самой паралельности.
     
    | Method                 | Mean     | Error    | StdDev   | Gen0   | Allocated |
    |----------------------- |---------:|---------:|---------:|-------:|----------:|
    | TestSquareParallelLinq | 69.48 ns | 0.474 ns | 0.420 ns | 0.0362 |     456 B |
    | TestSquareLinq         | 16.30 ns | 0.274 ns | 0.228 ns | 0.0089 |     112 B |
     */
    [Benchmark]
    public void TestSquareParallelLinq() => _numbers.AsParallel().Select(Calculations);
    [Benchmark]
    public void TestSquareLinq() => _numbers.Select(Calculations);
    private int Calculations(int number) => number * number;
    
    
    
    private readonly IEnumerable<int> _source = Enumerable.Range(1, 10000000);
    /*
    Значений гораздо больше и паралельность оправдывается за счет количества.

    | Method                 | Mean     | Error    | StdDev   | Gen0      | Gen1      | Gen2      | Allocated |
    |----------------------- |---------:|---------:|---------:|----------:|----------:|----------:|----------:|
    | TestSquareParallelLinq | 21.87 ms | 0.133 ms | 0.124 ms | 2937.5000 | 2937.5000 | 2937.5000 |     64 MB |
    | TestSquareLinq         | 46.70 ms | 0.928 ms | 2.462 ms | 2272.7273 | 2272.7273 | 2272.7273 | 128.02 MB |
    */
    [Benchmark]
    public void TestLargeSquareParallelLinq()
    {
        var result = _source.Where(x => x % 2 == 0).Select(x => x * x).ToList();
    }

    [Benchmark]
    public void TestLargeSquareLinq()
    {
        var result = _source.AsParallel().Where(x => x % 2 == 0).Select(x => x * x).ToList();
    }
}