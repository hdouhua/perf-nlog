using System.Threading;
using BenchmarkDotNet.Attributes;
using NLog;

namespace Perf_Nlog
{
    partial class BaseTest
    {
        private static int _stringLogIndex;

        [Benchmark]
        public object CreateFromString()
        {
            return LogManager.GetLogger("my-logger_" + (Interlocked.Increment(ref _stringLogIndex) % 1000));
        }

        [Benchmark]
        public object CreateTypeOfLogger()
        {
            return new[]
            {
                LogManager.GetCurrentClassLogger(typeof(BaseTest)),
                LogManager.GetCurrentClassLogger(typeof(BaseTest)),
                LogManager.GetCurrentClassLogger(typeof(BaseTest)),
                LogManager.GetCurrentClassLogger(typeof(BaseTest)),

                LogManager.GetCurrentClassLogger(typeof(BaseTest)),
                LogManager.GetCurrentClassLogger(typeof(BaseTest)),
                LogManager.GetCurrentClassLogger(typeof(BaseTest)),
                LogManager.GetCurrentClassLogger(typeof(BaseTest)),

                LogManager.GetCurrentClassLogger(typeof(BaseTest)),
                LogManager.GetCurrentClassLogger(typeof(BaseTest)),
                LogManager.GetCurrentClassLogger(typeof(BaseTest)),
                LogManager.GetCurrentClassLogger(typeof(BaseTest)),

                LogManager.GetCurrentClassLogger(typeof(BaseTest)),
                LogManager.GetCurrentClassLogger(typeof(BaseTest)),
                LogManager.GetCurrentClassLogger(typeof(BaseTest)),
                LogManager.GetCurrentClassLogger(typeof(BaseTest)),
            };
        }

        [Benchmark]
        public object CreateDynamicLogger()
        {
            return new[]
            {
                LogManager.GetCurrentClassLogger(),
                LogManager.GetCurrentClassLogger(),
                LogManager.GetCurrentClassLogger(),
                LogManager.GetCurrentClassLogger(),

                LogManager.GetCurrentClassLogger(),
                LogManager.GetCurrentClassLogger(),
                LogManager.GetCurrentClassLogger(),
                LogManager.GetCurrentClassLogger(),

                LogManager.GetCurrentClassLogger(),
                LogManager.GetCurrentClassLogger(),
                LogManager.GetCurrentClassLogger(),
                LogManager.GetCurrentClassLogger(),

                LogManager.GetCurrentClassLogger(),
                LogManager.GetCurrentClassLogger(),
                LogManager.GetCurrentClassLogger(),
                LogManager.GetCurrentClassLogger(),
            };
        }

    }
}