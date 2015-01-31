using System.Collections.Generic;
using SpecResults.Model;
using TechTalk.SpecFlow;
using ScenarioBlock = SpecResults.Model.ScenarioBlock;

namespace SpecResults
{
	public static partial class Reporters
	{
		private static bool _testrunIsFirstFeature;

		[AfterFeature]
		internal static void AfterFeature()
		{
			foreach (var reporter in reporters)
			{
				var feature = reporter.CurrentFeature;
				feature.EndTime = CurrentRunTime;
				OnFinishedFeature(reporter);
				reporter.CurrentFeature = null;
			}
		}

		[AfterScenario]
		internal static void AfterScenario()
		{
			foreach (var reporter in reporters.ToArray())
			{
				var scenario = reporter.CurrentScenario;
				scenario.EndTime = CurrentRunTime;
				OnFinishedScenario(reporter);
				reporter.CurrentScenario = null;
			}
		}

		[AfterScenarioBlock]
		internal static void AfterScenarioBlock()
		{
			var endtime = CurrentRunTime;
			foreach (var reporter in reporters)
			{
				var scenarioblock = reporter.CurrentScenarioBlock;
				scenarioblock.EndTime = endtime;
				OnFinishedScenarioBlock(reporter);
				reporter.CurrentScenarioBlock = null;
			}
		}

		[AfterTestRun]
		internal static void AfterTestRun()
		{
			foreach (var reporter in reporters)
			{
				reporter.Report.EndTime = CurrentRunTime;
				OnFinishedReport(reporter);
			}
		}

		[BeforeFeature]
		internal static void BeforeFeature()
		{
			var starttime = CurrentRunTime;

			// Init reports when the first feature runs. This is intentionally not done in
			// BeforeTestRun(), to make sure other [BeforeTestRun] annotated methods can perform
			// initialization before the reports are created.
			if (_testrunIsFirstFeature)
			{
				foreach (var reporter in reporters)
				{
					reporter.Report = new Report
					{
						Features = new List<Feature>(),
						Generator = reporter.Name,
						StartTime = starttime
					};

					OnStartedReport(reporter);
				}

				_testrunIsFirstFeature = false;
			}

			foreach (var reporter in reporters)
			{
				var feature = new Feature
				{
					Tags = new List<string>(FeatureContext.Current.FeatureInfo.Tags),
					Scenarios = new List<Scenario>(),
					StartTime = starttime,
					Title = FeatureContext.Current.FeatureInfo.Title,
					Description = FeatureContext.Current.FeatureInfo.Description
				};

				reporter.Report.Features.Add(feature);
				reporter.CurrentFeature = feature;

				OnStartedFeature(reporter);
			}
		}

		[BeforeScenario]
		internal static void BeforeScenario()
		{
			var starttime = CurrentRunTime;

			foreach (var reporter in reporters)
			{
				var scenario = new Scenario
				{
					Tags = new List<string>(ScenarioContext.Current.ScenarioInfo.Tags),
					Given = new ScenarioBlock {Steps = new List<Step>()},
					When = new ScenarioBlock {Steps = new List<Step>()},
					Then = new ScenarioBlock {Steps = new List<Step>()},
					StartTime = starttime,
					Title = ScenarioContext.Current.ScenarioInfo.Title
				};

				reporter.CurrentFeature.Scenarios.Add(scenario);
				reporter.CurrentScenario = scenario;

				OnStartedScenario(reporter);
			}
		}

		[BeforeScenarioBlock]
		internal static void BeforeScenarioBlock()
		{
			var starttime = CurrentRunTime;

			foreach (var reporter in reporters)
			{
				switch (ScenarioContext.Current.CurrentScenarioBlock)
				{
					case TechTalk.SpecFlow.ScenarioBlock.Given:
						reporter.CurrentScenarioBlock = reporter.CurrentScenario.Given;
						break;
					case TechTalk.SpecFlow.ScenarioBlock.Then:
						reporter.CurrentScenarioBlock = reporter.CurrentScenario.Then;
						break;
					case TechTalk.SpecFlow.ScenarioBlock.When:
						reporter.CurrentScenarioBlock = reporter.CurrentScenario.When;
						break;
				}

				reporter.CurrentScenarioBlock.StartTime = starttime;
				OnStartedScenarioBlock(reporter);
			}
		}

		[BeforeTestRun]
		internal static void BeforeTestRun()
		{
			_testrunIsFirstFeature = true;
		}
	}
}