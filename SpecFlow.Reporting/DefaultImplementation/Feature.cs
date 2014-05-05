using System.Collections.Generic;

namespace SpecFlow.Reporting
{
	public class Feature : TaggedReportItem, IFeature
	{
		public string Description { get; set; }

		public List<IScenario> Scenarios { get; set; }

		public override TestResult Result
		{
			get { return Scenarios.GetResult(); }
		}
	}
}