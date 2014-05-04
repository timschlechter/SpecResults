using System.Collections.Generic;

namespace SpecFlow.Reporting
{
	public abstract class BaseReportFactory : IReportingFactory
	{
		public abstract IReport DoCreateReport();

		public abstract IFeature DoCreateFeature();

		public abstract IScenario DoCreateScenario();

		public abstract IScenarioBlock DoCreateScenarioBlock();

		public abstract IStep DoCreateStep();

		public IReport CreateReport()
		{
			var report = DoCreateReport();

			report.Features = new List<IFeature>();

			return report;
		}

		public IFeature CreateFeature()
		{
			var feature = DoCreateFeature();

			feature.Tags = new List<string>();
			feature.Scenarios = new List<IScenario>();

			return feature;
		}

		public IScenario CreateScenario()
		{
			var scenario = DoCreateScenario();

			scenario.Tags = new List<string>();
			scenario.Given = CreateScenarioBlock();
			scenario.When = CreateScenarioBlock();
			scenario.Then = CreateScenarioBlock();

			return scenario;
		}

		public IScenarioBlock CreateScenarioBlock()
		{
			return DoCreateScenarioBlock();
		}

		public IStep CreateStep()
		{
			var step = DoCreateStep();

			step.Steps = new List<IStep>();

			return step;
		}
	}
}