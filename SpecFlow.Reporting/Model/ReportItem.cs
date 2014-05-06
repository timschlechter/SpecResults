using System;

namespace SpecFlow.Reporting
{
	public abstract class ReportItem
	{
		public string Title { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }

		public object UserData { get; set; }

		public virtual TestResult Result { get; set; }

		protected ReportItem()
		{
		}
	}
}