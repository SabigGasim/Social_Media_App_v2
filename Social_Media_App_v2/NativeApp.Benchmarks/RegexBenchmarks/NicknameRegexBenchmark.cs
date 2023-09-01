
using BenchmarkDotNet.Attributes;
using NativeApp.Validations.RegularExpressions;

namespace NativeApp.Benchmarks.RegexBenchmarks;

/*
BenchmarkDotNet v0.13.7, Windows 11 (10.0.22621.2134/22H2/2022Update/SunValley2)
Intel Core i5-10400F CPU 2.90GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 7.0.400
  [Host]     : .NET 7.0.10 (7.0.1023.36312), X64 RyuJIT AVX2 [AttachedDebugger]
  DefaultJob : .NET 7.0.10 (7.0.1023.36312), X64 RyuJIT AVX2

|                Method |      Mean |    Error |   StdDev | Allocated |
|---------------------- |----------:|---------:|---------:|----------:|
|          SucceedMatch |  90.52 ns | 0.135 ns | 0.113 ns |         - |
|    FailMatch_EndError | 100.18 ns | 0.410 ns | 0.343 ns |         - |
| FailMatch_MiddleError |  56.11 ns | 0.177 ns | 0.157 ns |         - |
|  FailMatch_StartError |  36.83 ns | 0.172 ns | 0.152 ns |         - |
 
*/

[MemoryDiagnoser]
public class NicknameRegexBenchmark
{
    [Benchmark]
    public bool SucceedMatch()
    {
        return NicknameRegex.IsMatch("Hello_हिन्दी  1345");
    }

    [Benchmark]
    public bool FailMatch_EndError()
    {
        return NicknameRegex.IsMatch("Hello_हिन्दी 1345-");
    }

    [Benchmark]
    public bool FailMatch_MiddleError()
    {
        return NicknameRegex.IsMatch("Hello_हि-न्दी 1345");
    }

    [Benchmark]
    public bool FailMatch_StartError()
    {
        return NicknameRegex.IsMatch("-Hello_हिन्दी 1345");
    }
}
