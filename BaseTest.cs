using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using BenchmarkDotNet.Attributes;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Targets.Wrappers;

namespace Perf_Nlog
{
    [MemoryDiagnoser]
    [MarkdownExporterAttribute.GitHub]
    [RankColumn]
    public partial class BaseTest
    {
        private const string DefaultLoggerName = "DefaultLogger";
        private const string DefaultMessage = "EFl1TjmLh5IV1L3dRV0T9M1IAJMPz46UUb8ufEHkqwDrqWx89SAUAiqPyLOH65VszJtQueFEEIUrpWbiIFX99V8wZ3uKS9RCVtKt7XpE8TP8HlekzCStjOsw9RdOIBrNkdy4jcRhvKEJzRyh5gClP9EH8T5jM4fnnIRjvsifFVuHubW2632XfGkA1gsdNjxLGkyMDJzRsPVUW3BssOmVITYuxa3B1yZZQAYxwvs5t8HZ0GtEzg1i8kAEgEGxQaDOi5o38OwOFioqRcKG08QR3zY52P5P5OdK27vqTOCSqBm08l7hBa94srLUUc1j3wT32DQgIbet2kFIBlopLgVo31nA16LTOBW4Fvvsqi4zfqQi11x7RdWefahWWgifPhyVQwp0ihHYQpRq3W821EPYaCVY6D0gSFDn3ht79eSPM6Xdg4bAdb3pAE4ykOHElxryfPCUVREECdF9KLUQyBsZD6pcJkfcm5WyES8FIa9LvFh9f1vJMJ9NNEbLozokwYGa6hvXTyCwKMGvUEMa4tKH07r1zsk6zZaznmaqF03sj7a5GZTiXfRvhAqzxsOFdqmjd0mAYj20iU8QhGoN4g6unkRbfhzBgEUup09fVdmzNxSyeYIifq6MYKmtG1My0khwUx2PxSjPdOJcSgPIr0iLpbuj6JeKMV6ZL3cPC6PlsewDa57U2zS1HLibxu8ENOLg9cshGHd6gkG90ngQiN5GCLqLKxsllRyD7yo7mPptOL6XCTXnu7Fe83qqfraCSBxW1Nobub9Obyg09Mo2gEooBeoz860LhZq6BBl0NtrYvgGdaU8fxNz7jgoE913bNhJYmFwy2mNiYckDYh9YHcEG8oen0PkaUdNqIvBPajghZq44NMllLir8pX9jnKLvivwHoojJCBYTu1xQ9RWcp11nxlAxSDce4kaaR0WY2CrJSFXR9Hu4Ws4DIBmJKwndrfLqGUSZndYFsjvbTGZEK31nk28z863MjInkDtlK7MFwPouF9oM1M6zmQ1oAL9Rk6pm73nVwjAfiOtWOTgpCiUMUVACcIJ3dA9hnjDsIEFpcw311mRXhZ5jbyVe4xRmixqme1gFk8q72jmZ7Lde0IJP2Hiu3b23SpByLumB3qyt8df0tcj6Avqr1JIKNG9Uo0YHEdO8qLUG7rjx6W83JPiKOX1wJ1G2HcKogXZxXcOqqWCma0GK3McIvoX0Rf7wwZTGYyrtey8LfW4zD6kEO0fBNSt8pYxeUR9AzVAR3MFLdTyuG6vuxsCoWcmRm5VRbRi02LPTOUVDwo7b8T0eXC3qKT8XDZr2gL7k3EnYHZa2jB8oRJM9OhCNH8Xp8SxMou4iQTuHS3lCGLEYIJ1M5xgrkz167jLPl1kbtk1B2tFbgL6lPyMXJBhopV8FV07hKpjKSdUVPtF0x9iIAtpSyOyrN9ePxarSUPJmalMDKVHPq97ysDGzPiNYJOs0km7o5M0u0jsiKv529fe8VhxMpwESvOy1HVzQX";

        private static int _logFileTargetIndex;
        private static readonly List<DirectoryInfo> Folders = new List<DirectoryInfo>();

        private Logger _optimizedSync;
        private Logger _concurrentWritesSync;
        private Logger _closeFileSync;
        private Logger _concurrentWritesAndCloseFileSync;
        private Logger _optimizedAsync;
        private Logger _concurrentWritesAsync;
        private Logger _closeFileAsync;
        private Logger _concurrentWritesAndCloseFileAsync;

        [Params(5000, 10000)]
        public int QueueL { get; set; }

        [Params(100, 200)]
        public int BatchS { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            var _logsFolder = CreateLogFolder();

            // from https://github.com/nlog/NLog/wiki/Configuration-API
            var config = new LoggingConfiguration();

            // var fileTarget = CreateNLogAppender(_logsFolder, concurrentWrites: false, keepFileOpen:true);
            // config.AddTarget(fileTarget);
            // config.AddRuleForOneLevel(LogLevel.Info, fileTarget, DefaultLoggerName);

            var fileTargetOptimizedSync = CreateNLogAppender(_logsFolder, concurrentWrites: false, keepFileOpen: true);
            var fileTargetWithConcurrentWritesSync = CreateNLogAppender(_logsFolder, concurrentWrites: true, keepFileOpen: true);
            var fileTargetWithCloseFileSync = CreateNLogAppender(_logsFolder, concurrentWrites: false, keepFileOpen: false);
            var fileTargetWithConcurrentWritesAndCloseFileSync = CreateNLogAppender(_logsFolder, concurrentWrites: true, keepFileOpen: false);
            var fileTargetOptimizedAsync = CreateNLogAppender(_logsFolder, concurrentWrites: false, keepFileOpen: true);
            var fileTargetWithConcurrentWritesAsync = CreateNLogAppender(_logsFolder, concurrentWrites: true, keepFileOpen: true);
            var fileTargetWithCloseFileAsync = CreateNLogAppender(_logsFolder, concurrentWrites: false, keepFileOpen: false);
            var fileTargetWithConcurrentWritesAndCloseFileAsync = CreateNLogAppender(_logsFolder, concurrentWrites: true, keepFileOpen: false);

            config.AddTarget(fileTargetOptimizedSync);
            config.AddTarget(fileTargetWithConcurrentWritesSync);
            config.AddTarget(fileTargetWithCloseFileSync);
            config.AddTarget(fileTargetWithConcurrentWritesAndCloseFileSync);
            config.AddTarget(fileTargetOptimizedAsync);
            config.AddTarget(fileTargetWithConcurrentWritesAsync);
            config.AddTarget(fileTargetWithCloseFileAsync);
            config.AddTarget(fileTargetWithConcurrentWritesAndCloseFileAsync);
            
            var overflowAction = AsyncTargetWrapperOverflowAction.Block;

            var asyncOptimized = new AsyncTargetWrapper(fileTargetOptimizedAsync)
            {
                Name = nameof(fileTargetOptimizedAsync),
                OverflowAction = overflowAction,
                QueueLimit = QueueL,
                BatchSize = BatchS
            };

            var asyncWithConcurrentWrites = new AsyncTargetWrapper(fileTargetWithConcurrentWritesAsync)
            {
                Name = nameof(fileTargetWithConcurrentWritesAsync),
                OverflowAction = overflowAction,
                QueueLimit = QueueL,
                BatchSize = BatchS
            };

            var asyncWithCloseFile = new AsyncTargetWrapper(fileTargetWithCloseFileAsync)
            {
                Name = nameof(fileTargetWithCloseFileAsync),
                OverflowAction = overflowAction,
                QueueLimit = QueueL,
                BatchSize = BatchS
            };

            var asyncWithConcurrentWritesAndCloseFile = new AsyncTargetWrapper(fileTargetWithConcurrentWritesAndCloseFileAsync)
            {
                Name = nameof(fileTargetWithConcurrentWritesAndCloseFileAsync),
                OverflowAction = overflowAction,
                QueueLimit = QueueL,
                BatchSize = BatchS
            };

            _optimizedSync = LogManager.GetLogger(nameof(fileTargetOptimizedSync));
            _concurrentWritesSync = LogManager.GetLogger(nameof(fileTargetWithConcurrentWritesSync));
            _closeFileSync = LogManager.GetLogger(nameof(fileTargetWithCloseFileSync));
            _concurrentWritesAndCloseFileSync = LogManager.GetLogger(nameof(fileTargetWithConcurrentWritesAndCloseFileSync));
            _optimizedAsync = LogManager.GetLogger(nameof(fileTargetOptimizedAsync));
            _concurrentWritesAsync = LogManager.GetLogger(nameof(fileTargetWithConcurrentWritesAsync));
            _closeFileAsync = LogManager.GetLogger(nameof(fileTargetWithCloseFileAsync));
            _concurrentWritesAndCloseFileAsync = LogManager.GetLogger(nameof(fileTargetWithConcurrentWritesAndCloseFileAsync));

            config.AddRuleForOneLevel(LogLevel.Info, fileTargetOptimizedSync, nameof(fileTargetOptimizedSync));
            config.AddRuleForOneLevel(LogLevel.Info, fileTargetWithConcurrentWritesSync, nameof(fileTargetWithConcurrentWritesSync));
            config.AddRuleForOneLevel(LogLevel.Info, fileTargetWithCloseFileSync, nameof(fileTargetWithCloseFileSync));
            config.AddRuleForOneLevel(LogLevel.Info, fileTargetWithConcurrentWritesAndCloseFileSync, nameof(fileTargetWithConcurrentWritesAndCloseFileSync));
            config.AddRuleForOneLevel(LogLevel.Info, asyncOptimized, nameof(fileTargetOptimizedAsync));
            config.AddRuleForOneLevel(LogLevel.Info, asyncWithConcurrentWrites, nameof(fileTargetWithConcurrentWritesAsync));
            config.AddRuleForOneLevel(LogLevel.Info, asyncWithCloseFile, nameof(fileTargetWithCloseFileAsync));
            config.AddRuleForOneLevel(LogLevel.Info, asyncWithConcurrentWritesAndCloseFile, nameof(fileTargetWithConcurrentWritesAndCloseFileAsync));

            LogManager.Configuration = config;
        }

        private static string CreateLogFolder()
        {
            var logsFolder = $"logs_{DateTime.UtcNow.Ticks}_{Process.GetCurrentProcess().Id}";

            if (Directory.Exists(logsFolder))
            {
                Directory.Delete(logsFolder, true);
            }

            Directory.CreateDirectory(logsFolder);

            var info = new DirectoryInfo(logsFolder);

            Folders.Add(info);

            return new DirectoryInfo(logsFolder).FullName;
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            LogManager.Shutdown();
            LogManager.Configuration = null;

            foreach (var directoryInfo in Folders)
            {
                directoryInfo.Delete(true);
            }

            Folders.Clear();
        }

        private static FileTarget CreateNLogAppender(
            string logsFolder,
            bool concurrentWrites,
            bool keepFileOpen)
        {
            var index = _logFileTargetIndex++;
            return new FileTarget($"target_{index}")
            {
                ArchiveNumbering = ArchiveNumberingMode.Sequence,
                ArchiveFileName = $"{logsFolder}/t_{index}.{{#}}.log",
                FileName = $"{logsFolder}/t_{index}.log",
                ArchiveAboveSize = 10 * 1000 * 1000,
                MaxArchiveFiles = 1000,
                AutoFlush = true,
                ConcurrentWrites = concurrentWrites,
                KeepFileOpen = keepFileOpen,
                Layout = "${longdate}|${threadid}|${level}|${logger}|${message} ${exception:format=tostring}"
            };
        }

    }
}