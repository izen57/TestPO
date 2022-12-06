using Logic;

using Model;

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
			foreach (var alarmClock in _alarmClockService.GetAllAlarmClocks())
				_alarmClockService.Delete(alarmClock.AlarmTime);

			_checkForTime.Elapsed += AlarmClockSignal;
		}

		/// <summary>
		/// 1. ��������� ���������� �� ������ ����� ������ �����;
		/// 2. �������� ��� ������������;
		/// 3. ��������� ���������� �� 4 ������ �����;
		/// 4. ��������� ������ �����;
		/// 5. �������� ������������ ����������.
		/// </summary>
		[TestMethod]
		public void Test()
		{
			if (_alarmClockService.GetAllAlarmClocks().Count != 0)
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

			while (DateTime.Now != alarmClock.AlarmTime)
				;

			DateTime oldTime = alarmClock.AlarmTime;
			alarmClock.AlarmTime = DateTime.Now+new TimeSpan(0, 4, 0);
			alarmClock.IsWorking = true;
			_alarmClockService.Edit(alarmClock, oldTime);
		}

		public void AlarmClockSignal(object sender, ElapsedEventArgs e)
		{
			foreach (var alarmClock in _alarmClockService.GetAllAlarmClocks())
				if (alarmClock.IsWorking &&
					DateTime.Now >= alarmClock.AlarmTime &&
					DateTime.Now <= alarmClock.AlarmTime+new TimeSpan(0, 0, 30))
				{
					System.Console.WriteLine($"��������� �� ����� {alarmClock.AlarmTime} ��������.");
					alarmClock.IsWorking = false;
					_alarmClockService.Edit(alarmClock, alarmClock.AlarmTime);
				}
		}
	}
}