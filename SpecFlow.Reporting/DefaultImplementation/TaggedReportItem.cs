using System.Collections.Generic;

namespace SpecFlow.Reporting
{
	public class TaggedReportItem : ReportItem, ITagged
	{
		public List<string> Tags { get; set; }
	}
}