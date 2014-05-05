using System.Collections.Generic;

namespace SpecFlow.Reporting
{
	public class Report : ReportItem, IReport
	{
		public string Generator { get; set; }

		public List<IFeature> Features { get; set; }

		public override TestResult Result
		{
			get { return Features.GetResult(); }
		}
	}
}