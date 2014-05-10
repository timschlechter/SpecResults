namespace SpecFlow.Reporting
{
	public class ScenarioEventArgs : FeatureEventArgs
	{
		public Scenario Scenario { get; internal set; }

		public ScenarioEventArgs(Reporter reporter)
			: base(reporter)
		{
			Scenario = Reporter.CurrentScenario;
		}
	}
}