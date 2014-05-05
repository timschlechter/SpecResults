using System.Collections.Generic;

namespace SpecFlow.Reporting
{
	public interface IFeature : IReportItem, ITagged
	{
		string Description { get; set; }
		List<IScenario> Scenarios { get; set; }
	}
}