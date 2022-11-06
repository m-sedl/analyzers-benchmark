solutions = [
    "litedb/LiteDB.sln",
    "NLog/src/NLog.sln",
    "moq4/Moq.sln",
    "btcpayserver/btcpayserver.sln",

    # "parallel-programming-2-thread-pool/ThreadPool.sln", -- .NET 4.x
    # "parallel-programming-2-sync/ThreadSynchronization.sln", -- .NET 4.x
    # "shadowsocks-windows/shadowsocks-windows.sln", -- windows only

    "parallel-programming-3-thread-pool/Task2.sln",
    "xunit",
    "parallel-programming-1-sync/synchronization.sln",
    "efcore",
    "nunit/nunit.sln",
    "AutoMapper/AutoMapper.sln",  # chmod +x src/AutoMapper/ApiCompat/PreBuild.sh
    "spbu-homeworks-1/Semester3/Homework1/MatrixMultiplication/MatrixMultiplication.sln",
    "spbu-homeworks-1/Semester3/Homework4/MyFTP/MyFTP.sln",
    "spbu-homeworks-1/Semester3/Homework5/MyNUnit/MyNUnit.sln",
    "spbu-homeworks-1/Semester3/Homework2/Lazy/Lazy.sln",
    "spbu-homeworks-1/Semester3/Homework3/ThreadPoolTask/ThreadPoolTask.sln",
    "spbu-homeworks-1/Semester2/Homework1/BurrowsWheeler/BurrowsWheeler.sln",
    "spbu-homeworks-1/Semester2/Homework1/Sorting/Sorting.sln",
    # "spbu-homeworks-1/Semester2/Homework7/Calculator/Calculator.sln", -- WinForms
    # "spbu-homeworks-1/Semester2/Homework7/Clock/Clock.sln", -- WinForms
    "spbu-homeworks-1/Semester2/Homework4/UniqueList/UniqueList.sln",
    "spbu-homeworks-1/Semester2/Homework4/ParseTree/ParseTree.sln",
    "spbu-homeworks-1/Semester2/Homework6/Game/Game.sln",
    "spbu-homeworks-1/Semester2/Homework6/MapFilterFold/MapFilterFold.sln",
    "spbu-homeworks-1/Semester2/Homework5/Calculator/Calculator.sln",
    "spbu-homeworks-1/Semester2/Homework5/Routers/Routers.sln",
    "spbu-homeworks-1/Semester2/Homework2/Calculator/Calculator.sln",
    "spbu-homeworks-1/Semester2/Homework8/BTree/BTree.sln",
    "spbu-homeworks-1/Semester2/Test/PriorityQueue/PriorityQueue.sln",
    "spbu-homeworks-1/Semester2/Test/RLEAlgorithm/RLEAlgorithm.sln",
    "spbu-homeworks-1/Semester2/Test/BubbleSort/BubbleSort.sln",
    # "spbu-homeworks-1/Semester2/Test/ProgressIndicator/ProgressIndicator.sln", -- WinForms
    "spbu-homeworks-1/Semester2/Homework3/BTree/BTree.sln",
    "spbu-homeworks-1/SeleniumDemo/SeleniumDemo.sln",
    "PowerShell/test/perf/dotnet-tools/ResultsComparer/ResultsComparer.sln",

    # непонятные проблемы со сборкой (надо использовать 7 sdk)
    # curl --output dotnet-install.sh  https://dotnet.microsoft.com/download/dotnet/scripts/v1/dotnet-install.sh
    # ./dotnet-install.sh --install-dir "dotnet" --version 7.0.100-rc.2.22477.23
    # "PowerShell/test/tools/TestAlc/TestAlc.sln",
    # "PowerShell/PowerShell.sln",

    "parallel-programming-1-thread-pool/thread-pool.sln"
    "BenchmarkDotNet",
    "ILSpy/ILSpy.XPlat.slnf"
]

bin_directories = [
    # "litedb/LiteDB.Stress/bin",
    # "litedb/LiteDB.Tests/bin",
    # "litedb/LiteDB.Benchmarks/bin",
    # "litedb/LiteDB/bin",
    # "litedb/LiteDB.Shell/bin",
    # "NLog/src/NLog.Database/bin",
    # "NLog/src/NLog/bin",
    # "NLog/src/NLog.WindowsEventLog/bin",
    # "NLog/src/NLog.OutputDebugString/bin",
    # "NLog/src/NLog.WindowsRegistry/bin",
    # "NLog/tests/NLog.WindowsRegistry.Tests/bin",
    # "NLog/tests/NLogAutoLoadExtension/bin",
    # "NLog/tests/SampleExtensions/bin",
    # "NLog/tests/NLog.UnitTests/bin",
    # "NLog/tests/NLog.Database.Tests/bin",
    # "NLog/tests/ManuallyLoadedExtension/bin",
    # "NLog/tests/PackageLoaderTestAssembly/bin",
    # "moq4/src/Moq/bin",
    # "moq4/tests/Moq.Tests.FSharpTypes/bin",
    # "moq4/tests/Moq.Tests.VisualBasic/bin",
    # "moq4/tests/Moq.Tests/bin",
    # "parallel-programming-2-thread-pool/ThreadPool/bin",
    # "btcpayserver/BTCPayServer.Client/bin",
    # "btcpayserver/BTCPayServer.Tests/bin",
    # "btcpayserver/BTCPayServer/bin",
    # "btcpayserver/BTCPayServer.Common/bin",
    # "btcpayserver/Plugins/BTCPayServer.Plugins.Custodians.FakeCustodian/bin",
    # "btcpayserver/BTCPayServer.Plugins.Test/bin",
    # "btcpayserver/BTCPayServer.Abstractions/bin",
    # "btcpayserver/BTCPayServer.PluginPacker/bin",
    # "btcpayserver/BTCPayServer.Data/bin",
    # "btcpayserver/BTCPayServer.Rating/bin",
    # "parallel-programming-3-thread-pool/hometask_2/bin",
    # "parallel-programming-3-thread-pool/MyThreadPool.Tests/bin",
    # "xunit/src/xunit.v2.tests/bin",
    # "xunit/src/xunit.v3.runner.utility/bin",
    # "xunit/src/xunit.v3.core.tests/bin",
    # "xunit/src/xunit.v3.common.tests/bin",
    # "xunit/src/xunit.v3.runner.tdnet/bin",
    # "xunit/src/xunit.v3.runner.common/bin",
    # "xunit/src/xunit.v3.assert.tests/bin",
    # "xunit/src/xunit.v3.assert.back-compat/xunit.v3.assert.cs6/bin",
    # "xunit/src/xunit.v3.assert.back-compat/xunit.v3.assert.cs6.span/bin",
    # "xunit/src/xunit.v3.assert.back-compat/xunit.v3.assert.cs9.on-all/bin",
    # "xunit/src/xunit.v3.assert.back-compat/xunit.v3.assert.cs6.valuetask/bin",
    # "xunit/src/xunit.v3.assert.back-compat/xunit.v3.assert.cs9.nullable/bin",
    # "xunit/src/xunit.v3.assert/bin",
    # "xunit/src/xunit.v3.runner.msbuild/bin",
    # "xunit/src/xunit.v3.runner.common.tests/bin",
    # "xunit/src/xunit.v3.runner.msbuild.tests/bin",
    # "xunit/src/xunit.v3.runner.utility.tests/bin",
    # "xunit/src/xunit.v3.runner.tdnet.tests/bin",
    # "xunit/src/xunit.v3.runner.console.tests/bin",
    # "xunit/src/xunit.v3.runner.console/bin",
    # "xunit/src/xunit.v3.runner.inproc.console/bin",
    # "xunit/src/xunit.v3.runner.inproc.console.tests/bin",
    # "xunit/src/xunit.v1.tests/bin",
    # "xunit/src/xunit.v3.core/bin",
    # "xunit/src/xunit.v3.common/bin",
    # "xunit/tools/builder/bin",
    # "parallel-programming-1-sync/synchronization/bin",
    # "nunit/bin",
    # "AutoMapper/src/IntegrationTests/bin",
    # "AutoMapper/src/AutoMapper/bin",
    # "AutoMapper/src/UnitTests/bin",
    # "AutoMapper/src/Benchmark/bin",
    # "spbu-homeworks-1/Semester3/Homework1/MatrixMultiplication/bin",
    # "spbu-homeworks-1/Semester3/Homework1/MatrixMultiplication.Tests/bin",
    # "spbu-homeworks-1/Semester3/Homework4/ServerApp/bin",
    # "spbu-homeworks-1/Semester3/Homework4/MyFTP.Tests/bin",
    # "spbu-homeworks-1/Semester3/Homework4/ClientApp/bin",
    # "spbu-homeworks-1/Semester3/Homework5/TestProject/bin",
    # "spbu-homeworks-1/Semester3/Homework5/MyNUnit.Tests/bin",
    # "spbu-homeworks-1/Semester3/Homework5/MyNUnit/bin",
    # "spbu-homeworks-1/Semester3/Homework5/Attributes/bin",
    # "spbu-homeworks-1/Semester3/Homework2/Lazy/bin",
    # "spbu-homeworks-1/Semester3/Homework2/Lazy.Tests/bin",
    # "spbu-homeworks-1/Semester3/Homework3/ThreadPoolTask.Tests/bin",
    # "spbu-homeworks-1/Semester3/Homework3/ThreadPoolTask/bin",
    # "spbu-homeworks-1/Semester2/Homework1/BurrowsWheeler/bin",
    # "spbu-homeworks-1/Semester2/Homework1/Sorting/bin",
    # "spbu-homeworks-1/Semester2/Homework7/CalculatingClass.Tests/bin",
    # "spbu-homeworks-1/Semester2/Homework4/ParseTree.Tests/bin",
    # "spbu-homeworks-1/Semester2/Homework4/UniqueList.Tests/bin",
    # "spbu-homeworks-1/Semester2/Homework4/UniqueList/bin",
    # "spbu-homeworks-1/Semester2/Homework4/ParseTree/bin",
    # "spbu-homeworks-1/Semester2/Homework6/GameTask.Tests/bin",
    # "spbu-homeworks-1/Semester2/Homework6/Game/bin",
    # "spbu-homeworks-1/Semester2/Homework6/MapFilterFold/bin",
    # "spbu-homeworks-1/Semester2/Homework6/MapFilterFold.Tests/bin",
    # "spbu-homeworks-1/Semester2/Homework5/Calculator/bin",
    # "spbu-homeworks-1/Semester2/Homework5/Routers/bin",
    # "spbu-homeworks-1/Semester2/Homework5/Calculator.Tests/bin",
    # "spbu-homeworks-1/Semester2/Homework5/Routers.Tests/bin",
    # "spbu-homeworks-1/Semester2/Homework2/Calculator/bin",
    # "spbu-homeworks-1/Semester2/Homework2/Calculator.Tests/bin",
    # "spbu-homeworks-1/Semester2/Homework8/BTree.Tests/bin",
    # "spbu-homeworks-1/Semester2/Homework8/BTree/bin",
    # "spbu-homeworks-1/Semester2/Test/PriorityQueue/bin",
    # "spbu-homeworks-1/Semester2/Test/RLEAlgorithm.Tests/bin",
    # "spbu-homeworks-1/Semester2/Test/RLEAlgorithm/bin",
    # "spbu-homeworks-1/Semester2/Test/BubbleSort/bin",
    # "spbu-homeworks-1/Semester2/Test/BubbleSort.Tests/bin",
    # "spbu-homeworks-1/Semester2/Test/PriorityQueue.Tests/bin",
    # "spbu-homeworks-1/Semester2/Homework3/BTree.Tests/bin",
    # "spbu-homeworks-1/Semester2/Homework3/BTree/bin",
    # "spbu-homeworks-1/SeleniumDemo/bin",
    # "PowerShell/test/xUnit/bin",
    # "PowerShell/test/perf/dotnet-tools/ResultsComparer/bin",
    # "PowerShell/src/Microsoft.PowerShell.ConsoleHost/bin",
    # "PowerShell/src/Microsoft.WSMan.Runtime/bin",
    # "PowerShell/src/Microsoft.PowerShell.Commands.Utility/bin",
    # "PowerShell/src/Microsoft.PowerShell.SDK/bin",
    # "PowerShell/src/powershell-unix/bin",
    # "PowerShell/src/System.Management.Automation/SourceGenerators/PSVersionInfoGenerator/bin",
    # "PowerShell/src/System.Management.Automation/bin",
    # "PowerShell/src/powershell-win-core/bin",
    # "PowerShell/src/Microsoft.PowerShell.Commands.Diagnostics/bin",
    # "PowerShell/src/Microsoft.Management.Infrastructure.CimCmdlets/bin",
    # "PowerShell/src/Microsoft.PowerShell.CoreCLR.Eventing/bin",
    # "PowerShell/src/Microsoft.WSMan.Management/bin",
    # "PowerShell/src/Microsoft.PowerShell.Security/bin",
    # "PowerShell/src/Microsoft.PowerShell.Commands.Management/bin",
    # "parallel-programming-1-thread-pool/thread-pool-tests/bin",
    # "parallel-programming-1-thread-pool/thread-pool/bin",
    
    # "efcore/artifacts/bin/EFCore",
    # "efcore/artifacts/bin/EFCore.Abstractions",
    # "efcore/artifacts/bin/EFCore.Analyzers",
    # "efcore/artifacts/bin/EFCore.Analyzers.Tests", --- ERROR

    # "efcore/artifacts/bin/EFCore.Benchmarks",
    # "efcore/artifacts/bin/EFCore.Cosmos",
    # "efcore/artifacts/bin/Microsoft.Data.Sqlite",
    # "efcore/artifacts/bin/Microsoft.Data.Sqlite.Core",
    # "efcore/artifacts/bin/EFCore.Design",
    # "efcore/artifacts/bin/EFCore.InMemory",
    # "efcore/artifacts/bin/EFCore.Proxies",
    # "efcore/artifacts/bin/EFCore.Relational",
    # "efcore/artifacts/bin/EFCore.Sqlite",
    # "efcore/artifacts/bin/EFCore.Sqlite.Benchmarks",
    # "efcore/artifacts/bin/EFCore.Sqlite.Core",
    # "efcore/artifacts/bin/EFCore.Sqlite.NTS",
    # "efcore/artifacts/bin/EFCore.SqlServer",
    # "efcore/artifacts/bin/EFCore.SqlServer.Benchmarks",
    # "efcore/artifacts/bin/EFCore.SqlServer.NTS",
    # "efcore/artifacts/bin/EFCore.Templates",
    # "efcore/artifacts/bin/EFCore.Tools",
    # "efcore/artifacts/bin/EFCore.Cosmos.FunctionalTests",

    # Зависает, больше не пробовал
    # "efcore/artifacts/bin/EFCore.Cosmos.Tests",
    # "efcore/artifacts/bin/EFCore.CrossStore.FunctionalTests",
    # "efcore/artifacts/bin/EFCore.Design.Tests",
    # "efcore/artifacts/bin/EFCore.InMemory.FunctionalTests",
    # "efcore/artifacts/bin/EFCore.InMemory.Tests",
    # "efcore/artifacts/bin/EFCore.OData.FunctionalTests",
    # "efcore/artifacts/bin/EFCore.Proxies.Tests",
    # "efcore/artifacts/bin/EFCore.Relational.Specification.Tests",
    # "efcore/artifacts/bin/EFCore.Relational.Tests",
    # "efcore/artifacts/bin/EFCore.Specification.Tests",
    # "efcore/artifacts/bin/EFCore.Sqlite.FunctionalTests",
    # "efcore/artifacts/bin/EFCore.Sqlite.Tests",
    # "efcore/artifacts/bin/EFCore.SqlServer.FunctionalTests",
    # "efcore/artifacts/bin/EFCore.SqlServer.Tests",
    # "efcore/artifacts/bin/EFCore.Tests",
    # "efcore/artifacts/bin/EFCore.Trimming.Tests",
    # "efcore/artifacts/bin/ef.Tests",
    # "efcore/artifacts/bin/Microsoft.Data.Sqlite.e_sqlcipher.Tests",
    # "efcore/artifacts/bin/Microsoft.Data.Sqlite.sqlite3.Tests",
    # "efcore/artifacts/bin/Microsoft.Data.Sqlite.Tests",
    # "efcore/artifacts/bin/Microsoft.Data.Sqlite.winsqlite3.Tests"
    # "efcore/artifacts/bin/EFCore.AspNet.InMemory.FunctionalTests",
    # "efcore/artifacts/bin/EFCore.AspNet.Specification.Tests",
    # "efcore/artifacts/bin/EFCore.AspNet.Sqlite.FunctionalTests",
    # "efcore/artifacts/bin/EFCore.AspNet.SqlServer.FunctionalTests",
    "BenchmarkDotNet/src/BenchmarkDotNet.Annotations/bin",
    "BenchmarkDotNet/src/BenchmarkDotNet.Diagnostics.Windows/bin",
    "BenchmarkDotNet/src/BenchmarkDotNet/bin",
    "BenchmarkDotNet/tests/BenchmarkDotNet.IntegrationTests.ConfigPerAssembly/bin",
    "BenchmarkDotNet/tests/BenchmarkDotNet.IntegrationTests.VisualBasic/bin",
    "BenchmarkDotNet/tests/BenchmarkDotNet.IntegrationTests.DisabledOptimizations/bin",
    "BenchmarkDotNet/tests/BenchmarkDotNet.IntegrationTests.FSharp/bin",
    "BenchmarkDotNet/tests/BenchmarkDotNet.IntegrationTests.EnabledOptimizations/bin",
    "BenchmarkDotNet/tests/BenchmarkDotNet.IntegrationTests.ManualRunning/bin",
    "BenchmarkDotNet/tests/BenchmarkDotNet.IntegrationTests/bin",
    "BenchmarkDotNet/tests/BenchmarkDotNet.IntegrationTests.CustomPaths/bin",
    "BenchmarkDotNet/tests/BenchmarkDotNet.Tests/bin",
    "BenchmarkDotNet/tests/BenchmarkDotNet.IntegrationTests.Static/bin",
    "BenchmarkDotNet/samples/BenchmarkDotNet.Samples.FSharp/bin",
    "BenchmarkDotNet/samples/BenchmarkDotNet.Samples/bin",
    "BenchmarkDotNet/samples/BenchmarkDotNet.Samples.Forms/bin",
    "ILSpy/ICSharpCode.ILSpyCmd/bin",
    "ILSpy/ILSpy-tests/mcs/5.23/bin",
    "ILSpy/ILSpy-tests/mcs/2.6.4/bin",
    "ILSpy/ICSharpCode.Decompiler/bin",
    "ILSpy/ICSharpCode.ILSpyX/bin",
    "ILSpy/ICSharpCode.Decompiler.TestRunner/bin",
    "ILSpy/ICSharpCode.Decompiler.PowerShell/bin",
    "ILSpy/ICSharpCode.Decompiler.Tests/bin",
    "OpenRA/bin",
    "Newtonsoft.Json/Src/Newtonsoft.Json/bin",
    "Newtonsoft.Json/Src/Newtonsoft.Json.Tests/bin",
    "Newtonsoft.Json/Src/Newtonsoft.Json.TestConsole/bin",
    "RestSharp/test/RestSharp.Tests.Integrated/bin",
    "RestSharp/test/RestSharp.Tests.Serializers.Xml/bin",
    "RestSharp/test/RestSharp.Tests.Shared/bin",
    "RestSharp/test/RestSharp.Tests.Legacy/bin",
    "RestSharp/test/RestSharp.Tests/bin",
    "RestSharp/test/RestSharp.InteractiveTests/bin",
    "RestSharp/test/RestSharp.Tests.Serializers.Json/bin",
    "RestSharp/test/RestSharp.Tests.Serializers.Csv/bin",
    "RestSharp/benchmarks/RestSharp.Benchmarks/bin",
    "RestSharp/src/RestSharp.Serializers.Xml/bin",
    "RestSharp/src/RestSharp/bin",
    "RestSharp/src/RestSharp.Serializers.NewtonsoftJson/bin",
    "RestSharp/src/RestSharp.Serializers.CsvHelper/bin",
]

HEADER = '\033[95m'
OKBLUE = '\033[94m'
OKCYAN = '\033[96m'
OKGREEN = '\033[92m'
WARNING = '\033[93m'
FAIL = '\033[91m'
ENDC = '\033[0m'
BOLD = '\033[1m'
UNDERLINE = '\033[4m'
