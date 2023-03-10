using Logic;

using Model;

using Repositories;

using RepositoriesImplementations;

using System.Drawing;

namespace IntegrationTests.AlarmClockIntegrationTests
{
	[TestClass]
	public class AlarmClockIntegrationTestDelete
	{
		[TestMethod]
		public void Test()
		{
			IAlarmClockRepo alarmClockRepo = new AlarmClockFileRepo();
			IAlarmClockService alarmClockService = new AlarmClockService(alarmClockRepo);
			DateTime dateTime = new(2022, 9, 14, 19, 15, 0);
			AlarmClock? check1 = new(dateTime, "check1", Color.FromName("black"), true);
			alarmClockService.Create(check1);

			alarmClockService.Delete(dateTime);

			Assert.IsNull(alarmClockService.GetAlarmClock(dateTime), "AlarmClockDelete");
		}
	}
}
