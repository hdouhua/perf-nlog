using BenchmarkDotNet.Attributes;

namespace Perf_Nlog
{
    partial class BaseTest
    {
        [Benchmark(Description = "KeepFileOpen=true, ConcurrentWrites=false, Async=true")]
        public void AsyncOptimized() => _optimizedAsync.Info(DefaultMessage);

        [Benchmark(Description = "KeepFileOpen=true, ConcurrentWrites=true, Async=true")]
        public void AsyncConcurrentWrites() => _concurrentWritesAsync.Info(DefaultMessage);

        [Benchmark(Description = "KeepFileOpen=false, ConcurrentWrites=false, Async=true")]
        public void AsyncCloseFile() => _closeFileAsync.Info(DefaultMessage);

        [Benchmark(Description = "KeepFileOpen=false, ConcurrentWrites=true, Async=true")]
        public void AsyncConcurrentWritesAndCloseFile() => _concurrentWritesAndCloseFileAsync.Info(DefaultMessage);
    }
}