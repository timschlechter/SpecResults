using System.Collections.Generic;

namespace SpecFlow.Reporting
{
	public class Step : ReportItem, IStep
	{
		public IList<IStep> Steps { get; set; }
	}
}