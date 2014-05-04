using System.Collections.Generic;

namespace SpecFlow.Reporting
{
	public class Feature : TaggedReportItem, IFeature
	{
		public List<IScenario> Scenarios { get; set; }
	}
}