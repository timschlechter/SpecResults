using System.Collections.Generic;

namespace SpecFlow.Reporting
{
	public class Report : ReportItem, IReport
	{
		public List<IFeature> Features { get; set; }
	}
}