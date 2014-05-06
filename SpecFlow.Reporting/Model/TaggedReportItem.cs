using System.Collections.Generic;

namespace SpecFlow.Reporting
{
	public class TaggedReportItem : ReportItem
	{
		public List<string> Tags { get; set; }
	}
}