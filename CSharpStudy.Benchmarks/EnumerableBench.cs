using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace CSharpStudy.Benchmarks
{
    public class EnumerableBench
    {
        [Benchmark]
        public int Enumerable() => Sum(EnumerableEx.Return(8));

        [Benchmark]
        public int Array() => Sum(new[] {8});

        private static int Sum(IEnumerable<int> data) => data.Sum();
    }

    public static class EnumerableEx
    {
        public static IEnumerable<TResult> Return<TResult>(TResult value)
        {
            yield return value;
        }
    }
}