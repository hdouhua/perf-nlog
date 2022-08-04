using BenchmarkDotNet.Attributes;

namespace Perf_Nlog
{
    partial class BaseTest
    {
        [Benchmark(Description = "KeepFileOpen=true, ConcurrentWrites=false, Async=false")]
        public void ConcurrentWriteOptimized() => _optimizedSync.Info(DefaultMessage);

        [Benchmark(Description = "KeepFileOpen=true, ConcurrentWrites=true, Async=false")]
        public void ConcurrentWriteAllowMultiple() => _concurrentWritesSync.Info(DefaultMessage);

        [Benchmark(Description = "KeepFileOpen=false, ConcurrentWrites=false, Async=false")]
        public void ConcurrentWriteCloseFile() => _closeFileSync.Info(DefaultMessage);

        [Benchmark(Description = "KeepFileOpen=false, ConcurrentWrites=true, Async=false")]
        public void ConcurrentWriteAllowMultipleAndCloseFile() => _concurrentWritesAndCloseFileSync.Info(DefaultMessage);
    }
}