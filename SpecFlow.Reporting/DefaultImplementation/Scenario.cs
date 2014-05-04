namespace SpecFlow.Reporting
{
	public class Scenario : TaggedReportItem, IScenario
	{
		public IScenarioBlock Given { get; set; }

		public IScenarioBlock When { get; set; }

		public IScenarioBlock Then { get; set; }
	}
}