namespace SpecFlow.Reporting
{
	public class DefaultReportFactory : BaseReportFactory
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

		public override IScenarioBlock DoCreateScenarioBlock()
		{
			return new ScenarioBlock();
		}

		public override IStep DoCreateStep()
		{
			return new Step();
		}
	}
}