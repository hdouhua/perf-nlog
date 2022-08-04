using BenchmarkDotNet.Attributes;
using NLog;

namespace Perf_Nlog
{
    partial class BaseTest
    {
        // [Benchmark]
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

        // [Benchmark]
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