namespace SpecFlow.Reporting
{
	public class Scenario : TaggedReportItem, IScenario
	{
		public IScenarioBlock Given { get; set; }

		public IScenarioBlock When { get; set; }

		public IScenarioBlock Then { get; set; }

		public override TestResult Result
		{
			get { return new[] { Given.Result, When.Result, Then.Result }.GetResult(); }
		}
	}
}