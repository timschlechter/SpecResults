using System.Collections.Generic;

namespace SpecFlow.Reporting
{
	public interface IStep : IReportItem
	{
		IList<IStep> Steps { get; set; }


		new TestResult Result { get; set; }
	}
}