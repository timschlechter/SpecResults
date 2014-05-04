using System.Collections.Generic;

namespace SpecFlow.Reporting
{
	public interface IFeature : IReportItem, ITagged
	{
		List<IScenario> Scenarios { get; set; }
	}
}