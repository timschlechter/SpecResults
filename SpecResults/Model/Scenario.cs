namespace SpecResults
{
	public class Scenario : TaggedReportItem
	{
		public ScenarioBlock Given { get; set; }

		public ScenarioBlock When { get; set; }

		public ScenarioBlock Then { get; set; }

		public override TestResult Result
		{
			get { return new[] { Given.Result, When.Result, Then.Result }.GetResult(); }
		}
	}
}