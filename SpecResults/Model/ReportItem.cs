using System;

namespace SpecResults
{
	public abstract class ReportItem
	{
		public string Title { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }

		public object UserData { get; set; }

		public object GeneratorData { get; set; }

		public virtual TestResult Result { get; set; }

		protected ReportItem()
		{
		}
	}
}