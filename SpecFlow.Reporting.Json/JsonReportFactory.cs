namespace SpecFlow.Reporting.Json
{
	public class JsonReportFactory : BaseReportFactory
	{
		public override IReport DoCreateReport()
		{
			return new Report();
		}

		public override IFeature DoCreateFeature()
		{
			return new Feature();
		}

		public override IScenario DoCreateScenario()
		{
			return new Scenario();
		}

		public override IStep DoCreateStep()
		{
			return new Step();
		}

		public override IScenarioBlock DoCreateScenarioBlock()
		{
			return new ScenarioBlock();
		}
	}
}