using System;

namespace SpecFlow.Reporting
{
	public class ReportItem : IReportItem
	{
		public string Title { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }

		public object UserData { get; set; }
	}
}