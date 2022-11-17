using Logic;

using Model;

using Repositories;

using RepositoriesImplementations;

using System.Drawing;

namespace TestPO.AlarmClockIntegrationTests
{
	[TestClass]
	public class AlarmClockIntegrationTestEdit
	{
		[TestMethod]
		public void Test()
		{
			IAlarmClockRepo alarmClockRepo = new AlarmClockFileRepo();
			IAlarmClockService alarmClockService = new AlarmClockService(alarmClockRepo);
			DateTime dateTime = new(2022, 9, 14, 19, 15, 0);

			AlarmClock check2 = new(dateTime, "check2", Color.FromName("yellow"), false);
			alarmClockService.Edit(check2, dateTime);

			Assert.IsNotNull(alarmClockService.GetAlarmClock(dateTime), "AlarmClockEdit");
		}
	}
}
