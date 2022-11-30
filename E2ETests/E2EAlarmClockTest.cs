using Logic;

using Model;

using Repositories;

using RepositoriesImplementations;

using System.Drawing;
using System.Timers;

using Timer = System.Timers.Timer;


namespace E2ETests
{
	[TestClass]
	public class E2EAlarmClockTest
	{
		IAlarmClockService _alarmClockService;
		Timer _checkForTime = new(7000);

		[TestInitialize]
		public void E2EAlarmClockTestInit()
		{
			_alarmClockService = new AlarmClockService(new AlarmClockFileRepo());
			_checkForTime.Elapsed += AlarmClockSignal;
		}

		//1) завести чтобы зазвонил через минуту после начала теста 
		//2) проверить что позвонил 
		//3) выставить на время равное «время гарантированного старта приложения»
		//4) ребут
		//5) проверить что зазвонил после ребута
		[TestMethod]
		public void Test()
		{
			if (_alarmClockService.GetAllAlarmClocks().First() != null)
			{
				AlarmClock newAlarmClock = _alarmClockService.GetAllAlarmClocks().First();

				while (newAlarmClock.IsWorking)
					;
				Assert.IsFalse(newAlarmClock.IsWorking);
			}

			AlarmClock alarmClock = new(
				DateTime.Now+new TimeSpan(0, 1, 0),
				"E2E Alarm Clock",
				Color.FromName("black"),
				true
			);

			_alarmClockService.Create(alarmClock);

			while (alarmClock.IsWorking)
				;

			if (alarmClock.IsWorking == false)
			{
				DateTime oldTime = alarmClock.AlarmTime;
				alarmClock.AlarmTime = DateTime.Now+new TimeSpan(0, 4, 0);
				alarmClock.IsWorking = true;
				_alarmClockService.Edit(alarmClock, oldTime);

				// повтор теста?
			}
		}

		public void AlarmClockSignal(object sender, ElapsedEventArgs e)
		{
			foreach (var alarmClock in _alarmClockService.GetAllAlarmClocks())
				if (alarmClock.IsWorking && DateTime.Now >= alarmClock.AlarmTime && DateTime.Now <= alarmClock.AlarmTime+new TimeSpan(0, 0, 30))
				{
					System.Console.WriteLine($"Будильник на время {alarmClock.AlarmTime} сработал.");
					alarmClock.IsWorking = false;
					_alarmClockService.Edit(alarmClock, alarmClock.AlarmTime);
				}
		}
	}
}