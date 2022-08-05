``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.17763.2867 (1809/October2018Update/Redstone5)
Intel Xeon Gold 6230 CPU 2.10GHz, 4 CPU, 4 logical and 4 physical cores
.NET SDK=6.0.202
  [Host]     : .NET Core 3.1.25 (CoreCLR 4.700.22.21202, CoreFX 4.700.22.21303), X64 RyuJIT
  DefaultJob : .NET Core 3.1.25 (CoreCLR 4.700.22.21202, CoreFX 4.700.22.21303), X64 RyuJIT


```
|                                                    Method | QueueL | BatchS |       Mean |     Error |     StdDev |     Median | Rank |  Gen 0 |  Gen 1 |  Gen 2 | Allocated |
|---------------------------------------------------------- |------- |------- |-----------:|----------:|-----------:|-----------:|-----:|-------:|-------:|-------:|----------:|
|   **&#39;KeepFileOpen=true, ConcurrentWrites=false, Async=true&#39;** |   **5000** |    **100** |   **9.165 μs** | **0.2632 μs** |  **0.7719 μs** |   **9.175 μs** |    **3** | **0.2594** | **0.1373** | **0.0305** |   **3,534 B** |
|    &#39;KeepFileOpen=true, ConcurrentWrites=true, Async=true&#39; |   5000 |    100 |   7.914 μs | 0.1718 μs |  0.5012 μs |   7.904 μs |    2 | 0.2441 | 0.1221 |      - |   3,560 B |
|  &#39;KeepFileOpen=false, ConcurrentWrites=false, Async=true&#39; |   5000 |    100 |  12.954 μs | 0.2587 μs |  0.4859 μs |  12.976 μs |    6 | 0.2594 | 0.1373 | 0.0153 |   3,526 B |
|   &#39;KeepFileOpen=false, ConcurrentWrites=true, Async=true&#39; |   5000 |    100 |  12.990 μs | 0.2597 μs |  0.5808 μs |  12.925 μs |    6 | 0.2594 | 0.1373 | 0.0305 |   3,578 B |
|  &#39;KeepFileOpen=true, ConcurrentWrites=false, Async=false&#39; |   5000 |    100 |  10.187 μs | 0.2012 μs |  0.3730 μs |  10.201 μs |    4 | 0.0153 |      - |      - |     266 B |
|   &#39;KeepFileOpen=true, ConcurrentWrites=true, Async=false&#39; |   5000 |    100 |  13.957 μs | 0.2778 μs |  0.6918 μs |  13.788 μs |    8 |      - |      - |      - |     259 B |
| &#39;KeepFileOpen=false, ConcurrentWrites=false, Async=false&#39; |   5000 |    100 | 286.655 μs | 5.6152 μs | 13.6681 μs | 286.022 μs |   10 |      - |      - |      - |   5,672 B |
|  &#39;KeepFileOpen=false, ConcurrentWrites=true, Async=false&#39; |   5000 |    100 | 287.332 μs | 5.6994 μs | 16.0753 μs | 284.926 μs |   10 |      - |      - |      - |   5,672 B |
|   **&#39;KeepFileOpen=true, ConcurrentWrites=false, Async=true&#39;** |   **5000** |    **200** |   **7.611 μs** | **0.1621 μs** |  **0.4755 μs** |   **7.603 μs** |    **1** | **0.2518** | **0.1221** | **0.0305** |   **3,540 B** |
|    &#39;KeepFileOpen=true, ConcurrentWrites=true, Async=true&#39; |   5000 |    200 |   7.498 μs | 0.1995 μs |  0.5883 μs |   7.398 μs |    1 | 0.2441 | 0.1221 | 0.0305 |   3,570 B |
|  &#39;KeepFileOpen=false, ConcurrentWrites=false, Async=true&#39; |   5000 |    200 |   9.771 μs | 0.1920 μs |  0.4255 μs |   9.801 μs |    4 | 0.2441 | 0.1221 |      - |   3,531 B |
|   &#39;KeepFileOpen=false, ConcurrentWrites=true, Async=true&#39; |   5000 |    200 |  10.265 μs | 0.2096 μs |  0.6047 μs |  10.074 μs |    4 | 0.2594 | 0.1373 | 0.0458 |   3,587 B |
|  &#39;KeepFileOpen=true, ConcurrentWrites=false, Async=false&#39; |   5000 |    200 |   9.964 μs | 0.1815 μs |  0.3131 μs |   9.870 μs |    4 | 0.0153 |      - |      - |     261 B |
|   &#39;KeepFileOpen=true, ConcurrentWrites=true, Async=false&#39; |   5000 |    200 |  13.511 μs | 0.2695 μs |  0.4790 μs |  13.386 μs |    7 | 0.0153 |      - |      - |     266 B |
| &#39;KeepFileOpen=false, ConcurrentWrites=false, Async=false&#39; |   5000 |    200 | 277.689 μs | 5.5145 μs |  8.9049 μs | 278.097 μs |   10 |      - |      - |      - |   5,672 B |
|  &#39;KeepFileOpen=false, ConcurrentWrites=true, Async=false&#39; |   5000 |    200 | 282.163 μs | 5.6193 μs | 10.4158 μs | 283.584 μs |   10 |      - |      - |      - |   5,672 B |
|   **&#39;KeepFileOpen=true, ConcurrentWrites=false, Async=true&#39;** |  **10000** |    **100** |   **8.960 μs** | **0.2360 μs** |  **0.6772 μs** |   **9.030 μs** |    **3** | **0.2899** | **0.1678** | **0.0458** |   **3,544 B** |
|    &#39;KeepFileOpen=true, ConcurrentWrites=true, Async=true&#39; |  10000 |    100 |   9.006 μs | 0.2597 μs |  0.7618 μs |   8.993 μs |    3 | 0.2747 | 0.1373 | 0.0305 |   3,560 B |
|  &#39;KeepFileOpen=false, ConcurrentWrites=false, Async=true&#39; |  10000 |    100 |  14.014 μs | 0.2745 μs |  0.3664 μs |  14.020 μs |    8 | 0.2747 | 0.1526 | 0.0458 |   3,501 B |
|   &#39;KeepFileOpen=false, ConcurrentWrites=true, Async=true&#39; |  10000 |    100 |  14.811 μs | 0.2922 μs |  0.4961 μs |  14.821 μs |    9 | 0.2899 | 0.1678 | 0.0458 |   3,556 B |
|  &#39;KeepFileOpen=true, ConcurrentWrites=false, Async=false&#39; |  10000 |    100 |  10.002 μs | 0.1973 μs |  0.3608 μs |   9.963 μs |    4 | 0.0153 |      - |      - |     266 B |
|   &#39;KeepFileOpen=true, ConcurrentWrites=true, Async=false&#39; |  10000 |    100 |  13.381 μs | 0.2619 μs |  0.4788 μs |  13.413 μs |    7 | 0.0153 |      - |      - |     270 B |
| &#39;KeepFileOpen=false, ConcurrentWrites=false, Async=false&#39; |  10000 |    100 | 275.195 μs | 5.2650 μs | 14.1440 μs | 274.924 μs |   10 |      - |      - |      - |   5,690 B |
|  &#39;KeepFileOpen=false, ConcurrentWrites=true, Async=false&#39; |  10000 |    100 | 278.296 μs | 5.5323 μs | 13.9809 μs | 275.660 μs |   10 |      - |      - |      - |   5,683 B |
|   **&#39;KeepFileOpen=true, ConcurrentWrites=false, Async=true&#39;** |  **10000** |    **200** |   **8.810 μs** | **0.2352 μs** |  **0.6936 μs** |   **8.793 μs** |    **3** | **0.2594** | **0.1373** | **0.0458** |   **3,525 B** |
|    &#39;KeepFileOpen=true, ConcurrentWrites=true, Async=true&#39; |  10000 |    200 |   8.777 μs | 0.2513 μs |  0.7369 μs |   8.715 μs |    3 | 0.2899 | 0.1678 | 0.0458 |   3,560 B |
|  &#39;KeepFileOpen=false, ConcurrentWrites=false, Async=true&#39; |  10000 |    200 |  11.699 μs | 0.2442 μs |  0.7201 μs |  11.659 μs |    5 | 0.3052 | 0.1831 | 0.0610 |   3,557 B |
|   &#39;KeepFileOpen=false, ConcurrentWrites=true, Async=true&#39; |  10000 |    200 |  11.610 μs | 0.2277 μs |  0.4652 μs |  11.569 μs |    5 | 0.2747 | 0.1526 | 0.0458 |   3,564 B |
|  &#39;KeepFileOpen=true, ConcurrentWrites=false, Async=false&#39; |  10000 |    200 |   9.758 μs | 0.1949 μs |  0.2976 μs |   9.730 μs |    4 | 0.0153 |      - |      - |     254 B |
|   &#39;KeepFileOpen=true, ConcurrentWrites=true, Async=false&#39; |  10000 |    200 |  13.430 μs | 0.2669 μs |  0.4744 μs |  13.456 μs |    7 | 0.0153 |      - |      - |     269 B |
| &#39;KeepFileOpen=false, ConcurrentWrites=false, Async=false&#39; |  10000 |    200 | 274.189 μs | 5.4145 μs | 11.1820 μs | 271.772 μs |   10 |      - |      - |      - |   5,672 B |
|  &#39;KeepFileOpen=false, ConcurrentWrites=true, Async=false&#39; |  10000 |    200 | 270.884 μs | 5.4005 μs | 13.9403 μs | 270.944 μs |   10 |      - |      - |      - |   5,683 B |
