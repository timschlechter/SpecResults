using System.Collections.Generic;

namespace SpecFlow.Reporting
{
	public class Step : ReportItem
	{
		public IList<Step> Steps { get; set; }
	}
}