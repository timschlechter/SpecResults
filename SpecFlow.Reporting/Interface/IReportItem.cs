using System;

namespace SpecFlow.Reporting
{
	public interface IReportItem
	{
		string Title { get; set; }

		DateTime StartTime { get; set; }

		DateTime EndTime { get; set; }

		object UserData { get; set; }
	}
}