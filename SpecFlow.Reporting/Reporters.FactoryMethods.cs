using System.Collections.Generic;

namespace SpecFlow.Reporting
{
	public partial class Reporters
	{
		internal static Report CreateReport()
		{
			return new Report
			{
				Features = new List<Feature>()
			};
		}

		internal static Feature CreateFeature()
		{
			return new Feature
			{
				Tags = new List<string>(),
				Scenarios = new List<Scenario>()
			};
		}

		internal static Scenario CreateScenario()
		{
			return new Scenario
			{
				Tags = new List<string>(),
				Given = new ScenarioBlock { Steps = new List<Step>() },
				When = new ScenarioBlock { Steps = new List<Step>() },
				Then = new ScenarioBlock { Steps = new List<Step>() }
			};
		}

		internal static Step CreateStep()
		{
			return new Step { Steps = new List<Step>() };
		}
	}
}