using Model;

using System.Drawing;


namespace TestPO
{
	public class AlarmClockBuilder
	{
		private DateTime AlarmTime { get; set; } = DateTime.Now;
		private string Name { get; set; } = "Test";
		private Color AlarmClockColor { get; set; } = Color.Blue;
		private bool IsWorking { get; set; } = false;

		public AlarmClock Build()
		{
			return new AlarmClock(AlarmTime, Name, AlarmClockColor, IsWorking);
		}

		public AlarmClockBuilder WithDateTime(DateTime time)
		{
			AlarmTime = time;
			return this;
		}

		public AlarmClockBuilder WithName(string name)
		{
			Name = name;
			return this;
		}

		public AlarmClockBuilder WithColor(Color color)
		{
			AlarmClockColor = color;
			return this;
		}

		public AlarmClockBuilder WithFlag(bool isWorking)
		{
			IsWorking= isWorking;
			return this;
		}
	}
}