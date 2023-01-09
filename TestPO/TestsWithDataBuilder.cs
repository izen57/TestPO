using Logic;

using Model;

using Moq;

using Repositories;

using System.Drawing;

namespace TestPO
{
	[TestClass]
	public class TestsWithDataBuilder
	{
		[TestMethod]
		public void TestCreateDataBuilder()
		{
			AlarmClock alarmClock = new AlarmClockBuilder()
				.WithName("name")
				.WithDateTime(new(2022, 9, 14, 19, 30, 0))
				.WithColor(Color.AliceBlue)
				.WithFlag(true)
				.Build();

			var repoMock = new Mock<IAlarmClockRepo>();
			repoMock.Setup(x => x.Create(alarmClock)).Verifiable();

			var service = new AlarmClockService(repoMock.Object);
			service.Create(alarmClock);

			repoMock.Verify(x => x.Create(alarmClock));
		}

		[TestMethod]
		public void TestEditDataBuilder()
		{
			DateTime dateTime = new(2022, 9, 14, 19, 30, 0);

			AlarmClock alarmClock = new AlarmClockBuilder()
				.WithName("name")
				.WithDateTime(new(2022, 10, 14, 19, 30, 0))
				.WithColor(Color.AliceBlue)
				.WithFlag(true)
				.Build();

			var repoMock = new Mock<IAlarmClockRepo>();
			repoMock.Setup(x => x.Edit(alarmClock, dateTime)).Verifiable();

			var service = new AlarmClockService(repoMock.Object);
			service.Edit(alarmClock, dateTime);

			repoMock.Verify(x => x.Edit(alarmClock, dateTime));
		}
	}
}
