using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace SpecFlow.Reporting
{
	public static partial class Reporters
	{
		[BeforeTestRun]
		internal static void BeforeTestRun()
		{
			testrunIsFirstFeature = true;
			testrunStarttime = CurrentRunTime;
		}

		[BeforeFeature]
		internal static void BeforeFeature()
		{
			var starttime = CurrentRunTime;

			// Init reports when the first feature runs. This is intentionally
			// not done in BeforeTestRun(), to make sure other
			// [BeforeTestRun] annotated methods can perform initialization
			// before the reports are created.
			if (testrunIsFirstFeature)
			{
				foreach (var reporter in reporters)
				{
					reporter.Report = new Report
					{
						Features = new List<Feature>(),
						Generator = reporter.Name,
						StartTime = starttime
					};

					RaiseEvent(StartedReport, reporter);
				}

				testrunIsFirstFeature = false;
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

				RaiseEvent(StartedFeature, reporter);
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
					Given = new ScenarioBlock { Steps = new List<Step>() },
					When = new ScenarioBlock { Steps = new List<Step>() },
					Then = new ScenarioBlock { Steps = new List<Step>() },
					StartTime = starttime,
					Title = ScenarioContext.Current.ScenarioInfo.Title
				};

				reporter.CurrentFeature.Scenarios.Add(scenario);
				reporter.CurrentScenario = scenario;

				RaiseEvent(StartedScenario, reporter);
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
					case TechTalk.SpecFlow.ScenarioBlock.Given: reporter.CurrentScenarioBlock = reporter.CurrentScenario.Given; break;
					case TechTalk.SpecFlow.ScenarioBlock.Then: reporter.CurrentScenarioBlock = reporter.CurrentScenario.Then; break;
					case TechTalk.SpecFlow.ScenarioBlock.When: reporter.CurrentScenarioBlock = reporter.CurrentScenario.When; break;
					default:
						break;
				}

				reporter.CurrentScenarioBlock.StartTime = starttime;
				RaiseEvent(StartedScenarioBlock, reporter);
			}
		}

		[BeforeStep]
		internal static void BeforeStep()
		{
		}

		[AfterStep]
		internal static void AfterStep()
		{
		}

		[AfterScenarioBlock]
		internal static void AfterScenarioBlock()
		{
			var endtime = CurrentRunTime;
			foreach (var reporter in reporters)
			{
				var scenarioblock = reporter.CurrentScenarioBlock;
				scenarioblock.EndTime = endtime;
				RaiseEvent(FinishedScenarioBlock, reporter);
				reporter.CurrentScenarioBlock = null;
			}
		}

		[AfterScenario]
		internal static void AfterScenario()
		{
			foreach (var reporter in reporters.ToArray())
			{
				var scenario = reporter.CurrentScenario;
				scenario.EndTime = CurrentRunTime;
				RaiseEvent(FinishedScenario, reporter);
				reporter.CurrentScenario = null;
			}
		}

		[AfterFeature]
		internal static void AfterFeature()
		{
			foreach (var reporter in reporters)
			{
				var feature = reporter.CurrentFeature;
				feature.EndTime = CurrentRunTime;
				RaiseEvent(FinishedFeature, reporter);
				reporter.CurrentFeature = null;
			}
		}

		[AfterTestRun]
		internal static void AfterTestRun()
		{
			foreach (var reporter in reporters)
			{
				reporter.Report.EndTime = CurrentRunTime;
				RaiseEvent(FinishedReport, reporter);
			}
		}
	}
}