using BenchmarkDotNet.Running;
using NativeApp.Benchmarks.RegexBenchmarks;


BenchmarkRunner.Run<NicknameRegexBenchmark>();