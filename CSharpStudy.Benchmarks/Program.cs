using BenchmarkDotNet.Running;

namespace CSharpStudy.Benchmarks
{
    internal class Program
    {
        private static void Main()
        {
            BenchmarkRunner.Run(typeof(Program).Assembly);
        }
    }
}