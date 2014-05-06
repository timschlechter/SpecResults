using System.Collections.Generic;

namespace SpecFlow.Reporting
{
	public class Report : ReportItem
	{
		public string Generator { get; set; }

		public List<Feature> Features { get; set; }

		public override TestResult Result
		{
			get { return Features.GetResult(); }
		}
	}
}