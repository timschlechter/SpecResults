using System.Collections.Generic;

namespace SpecFlow.Reporting
{
	public interface IReport : IReportItem
	{
		List<IFeature> Features { get; set; }
	}
}