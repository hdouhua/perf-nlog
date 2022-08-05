``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.17763.2867 (1809/October2018Update/Redstone5)
Intel Xeon Gold 6230 CPU 2.10GHz, 4 CPU, 4 logical and 4 physical cores
.NET SDK=6.0.202
  [Host]     : .NET Core 3.1.25 (CoreCLR 4.700.22.21202, CoreFX 4.700.22.21303), X64 RyuJIT
  DefaultJob : .NET Core 3.1.25 (CoreCLR 4.700.22.21202, CoreFX 4.700.22.21303), X64 RyuJIT


```
|                                                    Method | QueueL | BatchS |       Mean |     Error |     StdDev |     Median | Rank |  Gen 0 |  Gen 1 |  Gen 2 | Allocated |
|---------------------------------------------------------- |------- |------- |-----------:|----------:|-----------:|-----------:|-----:|-------:|-------:|-------:|----------:|
|   **&#39;KeepFileOpen=true, ConcurrentWrites=false, Async=true&#39;** |   **5000** |    **100** |   **8.097 μs** | **0.3530 μs** |  **1.0354 μs** |   **8.208 μs** |    **3** | **0.2594** | **0.1373** | **0.0458** |   **3,544 B** |
|    &#39;KeepFileOpen=true, ConcurrentWrites=true, Async=true&#39; |   5000 |    100 |   7.075 μs | 0.1992 μs |  0.5843 μs |   7.113 μs |    2 | 0.2441 | 0.1221 | 0.0153 |   3,570 B |
|  &#39;KeepFileOpen=false, ConcurrentWrites=false, Async=true&#39; |   5000 |    100 |  12.386 μs | 0.2464 μs |  0.4687 μs |  12.378 μs |    8 | 0.2441 | 0.1221 |      - |   3,525 B |
|   &#39;KeepFileOpen=false, ConcurrentWrites=true, Async=true&#39; |   5000 |    100 |  12.874 μs | 0.2572 μs |  0.5859 μs |  12.866 μs |    9 | 0.2594 | 0.1373 | 0.0458 |   3,579 B |
|  &#39;KeepFileOpen=true, ConcurrentWrites=false, Async=false&#39; |   5000 |    100 |  10.132 μs | 0.1976 μs |  0.3409 μs |  10.115 μs |    5 | 0.0153 |      - |      - |     262 B |
|   &#39;KeepFileOpen=true, ConcurrentWrites=true, Async=false&#39; |   5000 |    100 |  13.507 μs | 0.2517 μs |  0.5732 μs |  13.489 μs |   10 |      - |      - |      - |     255 B |
| &#39;KeepFileOpen=false, ConcurrentWrites=false, Async=false&#39; |   5000 |    100 | 275.152 μs | 6.2435 μs | 18.4092 μs | 273.668 μs |   11 |      - |      - |      - |   5,672 B |
|  &#39;KeepFileOpen=false, ConcurrentWrites=true, Async=false&#39; |   5000 |    100 | 271.782 μs | 5.6044 μs | 16.5248 μs | 271.548 μs |   11 |      - |      - |      - |   5,686 B |
|   **&#39;KeepFileOpen=true, ConcurrentWrites=false, Async=true&#39;** |   **5000** |    **200** |   **6.755 μs** | **0.1697 μs** |  **0.4949 μs** |   **6.730 μs** |    **1** | **0.2441** | **0.1221** | **0.0153** |   **3,534 B** |
|    &#39;KeepFileOpen=true, ConcurrentWrites=true, Async=true&#39; |   5000 |    200 |   7.275 μs | 0.1544 μs |  0.4528 μs |   7.153 μs |    2 | 0.2441 | 0.1221 | 0.0305 |   3,571 B |
|  &#39;KeepFileOpen=false, ConcurrentWrites=false, Async=true&#39; |   5000 |    200 |   9.507 μs | 0.1891 μs |  0.4675 μs |   9.496 μs |    5 | 0.2594 | 0.1373 | 0.0305 |   3,531 B |
|   &#39;KeepFileOpen=false, ConcurrentWrites=true, Async=true&#39; |   5000 |    200 |   9.599 μs | 0.1910 μs |  0.4501 μs |   9.506 μs |    5 | 0.2594 | 0.1373 | 0.0305 |   3,569 B |
|  &#39;KeepFileOpen=true, ConcurrentWrites=false, Async=false&#39; |   5000 |    200 |   9.784 μs | 0.1949 μs |  0.3256 μs |   9.815 μs |    5 | 0.0153 |      - |      - |     259 B |
|   &#39;KeepFileOpen=true, ConcurrentWrites=true, Async=false&#39; |   5000 |    200 |  13.066 μs | 0.2574 μs |  0.6118 μs |  12.968 μs |    9 |      - |      - |      - |     256 B |
| &#39;KeepFileOpen=false, ConcurrentWrites=false, Async=false&#39; |   5000 |    200 | 273.826 μs | 5.4313 μs | 11.2165 μs | 274.594 μs |   11 |      - |      - |      - |   5,672 B |
|  &#39;KeepFileOpen=false, ConcurrentWrites=true, Async=false&#39; |   5000 |    200 | 267.288 μs | 5.3166 μs | 14.2827 μs | 266.839 μs |   11 |      - |      - |      - |   5,684 B |
|   **&#39;KeepFileOpen=true, ConcurrentWrites=false, Async=true&#39;** |  **10000** |    **100** |   **8.800 μs** | **0.2325 μs** |  **0.6783 μs** |   **8.864 μs** |    **4** | **0.2747** | **0.1373** | **0.0458** |   **3,534 B** |
|    &#39;KeepFileOpen=true, ConcurrentWrites=true, Async=true&#39; |  10000 |    100 |   8.801 μs | 0.2121 μs |  0.6187 μs |   8.815 μs |    4 | 0.3052 | 0.1831 | 0.0610 |   3,570 B |
|  &#39;KeepFileOpen=false, ConcurrentWrites=false, Async=true&#39; |  10000 |    100 |  13.580 μs | 0.2669 μs |  0.3375 μs |  13.575 μs |   10 | 0.2899 | 0.1678 | 0.0458 |   3,502 B |
|   &#39;KeepFileOpen=false, ConcurrentWrites=true, Async=true&#39; |  10000 |    100 |  13.765 μs | 0.2730 μs |  0.6162 μs |  13.817 μs |   10 | 0.2747 | 0.1526 | 0.0305 |   3,546 B |
|  &#39;KeepFileOpen=true, ConcurrentWrites=false, Async=false&#39; |  10000 |    100 |  10.020 μs | 0.2003 μs |  0.4396 μs |  10.011 μs |    5 | 0.0153 |      - |      - |     279 B |
|   &#39;KeepFileOpen=true, ConcurrentWrites=true, Async=false&#39; |  10000 |    100 |  13.661 μs | 0.2706 μs |  0.5015 μs |  13.566 μs |   10 | 0.0153 |      - |      - |     270 B |
| &#39;KeepFileOpen=false, ConcurrentWrites=false, Async=false&#39; |  10000 |    100 | 274.633 μs | 5.4590 μs | 14.2852 μs | 272.401 μs |   11 |      - |      - |      - |   5,672 B |
|  &#39;KeepFileOpen=false, ConcurrentWrites=true, Async=false&#39; |  10000 |    100 | 277.795 μs | 5.5000 μs | 14.7753 μs | 278.116 μs |   11 |      - |      - |      - |   5,693 B |
|   **&#39;KeepFileOpen=true, ConcurrentWrites=false, Async=true&#39;** |  **10000** |    **200** |   **8.644 μs** | **0.2728 μs** |  **0.8045 μs** |   **8.755 μs** |    **4** | **0.2747** | **0.1373** | **0.0458** |   **3,534 B** |
|    &#39;KeepFileOpen=true, ConcurrentWrites=true, Async=true&#39; |  10000 |    200 |   8.687 μs | 0.1768 μs |  0.5184 μs |   8.733 μs |    4 | 0.2747 | 0.1526 | 0.0458 |   3,560 B |
|  &#39;KeepFileOpen=false, ConcurrentWrites=false, Async=true&#39; |  10000 |    200 |  10.618 μs | 0.2044 μs |  0.2931 μs |  10.648 μs |    6 | 0.2747 | 0.1526 | 0.0458 |   3,501 B |
|   &#39;KeepFileOpen=false, ConcurrentWrites=true, Async=true&#39; |  10000 |    200 |  11.490 μs | 0.2293 μs |  0.5581 μs |  11.402 μs |    7 | 0.3052 | 0.1831 | 0.0610 |   3,584 B |
|  &#39;KeepFileOpen=true, ConcurrentWrites=false, Async=false&#39; |  10000 |    200 |  10.355 μs | 0.2037 μs |  0.4515 μs |  10.230 μs |    5 | 0.0153 |      - |      - |     279 B |
|   &#39;KeepFileOpen=true, ConcurrentWrites=true, Async=false&#39; |  10000 |    200 |  13.900 μs | 0.2699 μs |  0.4040 μs |  13.857 μs |   10 | 0.0153 |      - |      - |     258 B |
| &#39;KeepFileOpen=false, ConcurrentWrites=false, Async=false&#39; |  10000 |    200 | 278.721 μs | 5.5277 μs | 14.9444 μs | 278.324 μs |   11 |      - |      - |      - |   5,690 B |
|  &#39;KeepFileOpen=false, ConcurrentWrites=true, Async=false&#39; |  10000 |    200 | 280.789 μs | 5.6136 μs | 13.4498 μs | 283.544 μs |   11 |      - |      - |      - |   5,672 B |
