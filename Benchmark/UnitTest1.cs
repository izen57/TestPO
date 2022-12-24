using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CsProj;
using BenchmarkDotNet.Toolchains.DotNetCli;
using BenchmarkDotNet.Toolchains.InProcess.Emit;

using Logic;

using Model;

using Repositories;

using RepositoriesImplementations;

using System.Drawing;

namespace Benchmark
{
	public class MyConfig: ManualConfig
	{
		public MyConfig()
		{
			//AddJob(Job.Default.WithToolchain(
			//	InProcessEmitToolchain.From(NetCoreAppSettings
			//		.NetCoreApp21
			//		.WithCustomDotNetCliPath(@"C:\dotnet\dotnet.exe", "OutOfProcessToolchain"))));

			AddJob(Job.ShortRun.WithToolchain(InProcessEmitToolchain.Instance));

		}
	}

	[RankColumn]
	public class TheEasiestBenchmark
	{
		[Benchmark(Description = "Get an alarm clock through the service")]
		public AlarmClock? AlarmClockFindByService()
		{
			IAlarmClockRepo alarmClockRepo = new AlarmClockFileRepo();
			IAlarmClockService alarmClockService = new AlarmClockService(alarmClockRepo);

			for (int i = 0; i < 5000; ++i)
			{
				AlarmClock alarmClock = new(
					new DateTime(i),
					$"Alarm clock {i}",
					Color.FromArgb(i),
					true
				);

				alarmClockService.Create(alarmClock);
			}

			return alarmClockService.GetAlarmClock(new DateTime(4000));
		}

		[Benchmark(Description = "Get an alarm clock through the 'find' method")]
		public AlarmClock? AlarmClockFindByList()
		{
			IAlarmClockRepo alarmClockRepo = new AlarmClockFileRepo();
			IAlarmClockService alarmClockService = new AlarmClockService(alarmClockRepo);

			for (int i = 0; i < 5000; ++i)
			{
				AlarmClock alarmClock = new(
					new DateTime(i),
					$"Alarm clock {i}",
					Color.FromArgb(i),
					true
				);

				alarmClockService.Create(alarmClock);
			}

			return alarmClockService
				.GetAllAlarmClocks()
				.Find(x => x.AlarmTime == new DateTime(4000));
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