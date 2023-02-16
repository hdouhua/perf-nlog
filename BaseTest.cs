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
        private const string DefaultMessage = "tD96Qraa0QYRUStQvPJ0zwaYnW47EdCCIQYyUsCsghR7WXLEWw6rJns4H1TTqtmVqkLRREAps1jz0iocx7bugcq0JJJ2ip0du8arZUgrUvOjL7ETB26N8Kjdw17hxYLhY4LDM5DAGalXRLpC6qhxyH7wp6sMeIymai5B59jgle6k2ecJlBFIHu1bXI2Wah8qtKSf5ee6WxZo784mx3tyofTRKili13kWo35keg3mkbT9UyXysOP7GHFjuEAKBcsbWdQo4nYJjMeKm9VOYR7JjGB2RgFnw3FvKLXRBVXxMKn5urIwObG4b8BGOuwp1zsZSRVSPsW8D6WViVfB8SHFr12F5gDxhkVDy0Wk7zDgcO5z0Xn4PKP9WquckuCM0EEBNeF2lUDb03IA97fj5P4z90KgU7ezbPQ0hV7AMxrWl7pmpnlvNjZjIsDP7Mn8H4J1s4e4G6Bh9ZTNzQj6vxCjIGHbeORQ8MVZceL1BPN0wdvJIpPcvAX24BiAjvqdVPixwV5eFOmXQEdIUbzgt8k7U1oRq4JCvCpDBQI5U1I8k2uDp4C5Ykhk1Wp1aE6GYaa7MTkdjrVV4qI17eZzbBzLrfUH";

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
                OpenFileCacheTimeout = 120,
                KeepFileOpen = keepFileOpen,
                Layout = "${longdate}|${threadid}|${level}|${logger}|${message} ${exception:format=tostring}"
            };
        }

    }
}