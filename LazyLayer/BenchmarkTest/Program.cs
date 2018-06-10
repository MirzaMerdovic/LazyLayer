using BenchmarkDotNet.Running;
using System;

namespace BenchmarkTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<LazyLayerDynamicBenchmarks>();

            Console.WriteLine(summary);
            Console.ReadKey();
        }
    }
}