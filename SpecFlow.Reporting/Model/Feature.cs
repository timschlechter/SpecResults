using System.Collections.Generic;

namespace SpecFlow.Reporting
{
	public class Feature : TaggedReportItem
	{
		public string Description { get; set; }

		public string DescriptionHtml { get { return Markdown.ToHtml(Description); } }

		public List<Scenario> Scenarios { get; set; }

		public override TestResult Result
		{
			get { return Scenarios.GetResult(); }
		}
	}
}