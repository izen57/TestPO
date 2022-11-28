﻿using Logic;

using Model;

using Repositories;

using RepositoriesImplementations;

using System.Drawing;

namespace TestPPO
{
	[TestClass]
	public class AlarmClockWithoutMoqTests
	{
		[TestMethod]
		public void AlarmClockTest()
		{
			IAlarmClockRepo alarmClockRepo = new AlarmClockFileRepo();
			IAlarmClockService alarmClockService = new AlarmClockService(alarmClockRepo);
			DateTime dateTime = new(2022, 9, 14, 19, 15, 0);

			AlarmClock? check1 = new(dateTime, "check1", Color.FromName("black"), true);
			alarmClockService.Create(check1);

			Assert.IsNotNull(alarmClockService.GetAlarmClock(dateTime), "AlarmClockCreate");

			AlarmClock check2 = new(dateTime, "check2", Color.FromName("yellow"), false);
			alarmClockService.Edit(check2, dateTime);
			Assert.IsNotNull(alarmClockService.GetAlarmClock(dateTime), "AlarmClockEdit");

			alarmClockService.Delete(new(2022, 9, 14, 19, 15, 1));
			Assert.AreEqual(0, alarmClockService.GetAllAlarmClocks().Count, "AlarmClockDelete");
		}
	}
}
