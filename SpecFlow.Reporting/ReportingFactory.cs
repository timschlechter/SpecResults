using System.Collections.Generic;

namespace SpecFlow.Reporting
{
	public abstract class ReportingFactory : IReportingFactory
	{
		public abstract string Name { get; }

		protected virtual IReport DoCreateReport()
		{
			return new Report
			{
				Generator = Name
			};
		}

		protected virtual IFeature DoCreateFeature()
		{
			return new Feature();
		}

		protected virtual IScenario DoCreateScenario()
		{
			return new Scenario();
		}

		protected virtual IScenarioBlock DoCreateScenarioBlock()
		{
			return new ScenarioBlock();
		}

		protected virtual IStep DoCreateStep()
		{
			return new Step();
		}

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
			scenario.Given.BlockType = TechTalk.SpecFlow.ScenarioBlock.Given;
			scenario.When = CreateScenarioBlock();
			scenario.When.BlockType = TechTalk.SpecFlow.ScenarioBlock.When;
			scenario.Then = CreateScenarioBlock();
			scenario.Then.BlockType = TechTalk.SpecFlow.ScenarioBlock.Then;

			return scenario;
		}

		public IScenarioBlock CreateScenarioBlock()
		{
			var scenarioblock = DoCreateScenarioBlock();

			scenarioblock.Steps = new List<IStep>();

			return scenarioblock;
		}

		public IStep CreateStep()
		{
			var step = DoCreateStep();

			step.Steps = new List<IStep>();

			return step;
		}
	}
}