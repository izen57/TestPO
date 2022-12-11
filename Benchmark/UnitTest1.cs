using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CsProj;
using BenchmarkDotNet.Toolchains.DotNetCli;

namespace Benchmark
{
	public class MyConfig: ManualConfig
	{
		public MyConfig()
		{
			AddJob(Job.Default.WithToolchain(CsProjCoreToolchain.From(NetCoreAppSettings
					.NetCoreApp21
					.WithCustomDotNetCliPath(@"C:\dotnet\dotnet.exe", "OutOfProcessToolchain"))));
		}
	}

	[RankColumn]
	public class TheEasiestBenchmark
	{
		[Benchmark(Description = "Summ100")]
		public int Test100()
		{
			return Enumerable.Range(1, 100).Sum();
		}

		[Benchmark(Description = "Summ200")]
		public int Test200()
		{
			return Enumerable.Range(1, 200).Sum();
		}
	}

	public class Program
	{
		public static void Main(string[] args)
		{
			BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
		}
	}
}