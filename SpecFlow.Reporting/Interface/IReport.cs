using System.Collections.Generic;

namespace SpecFlow.Reporting
{
	public interface IReport : IReportItem
	{
		string Generator { get; set; }

		List<IFeature> Features { get; set; }
	}
}