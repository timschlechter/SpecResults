using System.Collections.Generic;

namespace SpecResults
{
	public class Feature : TaggedReportItem
	{
        private string _description;
		public string Description
		{
			get { return _description; }
			set { _description = string.IsNullOrEmpty(value) ? value : value.Replace("\r", ""); }
		}

		public string DescriptionHtml { get { return Markdown.ToHtml(Description); } }

		public List<Scenario> Scenarios { get; set; }

		public override TestResult Result
		{
			get { return Scenarios.GetResult(); }
		}
	}
}