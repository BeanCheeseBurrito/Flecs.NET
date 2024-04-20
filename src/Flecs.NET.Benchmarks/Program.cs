using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

// [assembly: SimpleJob(runtimeMoniker: RuntimeMoniker.Net70, launchCount: 1, warmupCount: 5, iterationCount: 10)]

namespace Flecs.NET.Benchmarks
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkSwitcher benchmark = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly);
            IConfig config = DefaultConfig.Instance.WithOptions(ConfigOptions.DisableOptimizationsValidator);

            benchmark.Run(args, config);
        }
    }
}
