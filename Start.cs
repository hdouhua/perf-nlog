using BenchmarkDotNet.Running;

namespace Perf_Nlog
{
    internal static class StartClass
    {
        private static void Main()
        {
            BenchmarkRunner.Run<BaseTest>();
        }
    }
}