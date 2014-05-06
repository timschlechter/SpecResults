using System.Linq;
using TechTalk.SpecFlow;

namespace SpecFlow.Reporting
{
	public static partial class Reporter
	{
		[BeforeTestRun]
		public static void BeforeTestRun()
		{
			testrunIsFirstFeature = true;
			testrunStarttime = CurrentRunTime;
		}

		[BeforeFeature]
		public static void BeforeFeature()
		{
			var starttime = CurrentRunTime;

			// Init reports when the first feature runs. This is intentionally
			// not done in BeforeTestRun(), to make sure other
			// [BeforeTestRun] annotated methods can perform initialization
			// before the reports are created.
			if (testrunIsFirstFeature)
			{
				var factories = factoryTypes
								.Select(type => (IReportingFactory)type.Assembly.CreateInstance(type.FullName))
								.ToList();

				// Register reporters
				reports.Clear();
				foreach (var factory in factories)
				{
					var report = factory.CreateReport();
					report.Generator = factory.Name;
					report.StartTime = starttime;
					var state = new ReportState
					{
						Report = report,
						Factory = factory
					};
					reports.Add(state);
					RaiseEvent(ReportStarted, state);
				}

				testrunIsFirstFeature = false;
			}

			foreach (var state in reports)
			{
				var feature = state.Factory.CreateFeature();
				feature.StartTime = starttime;
				feature.Title = FeatureContext.Current.FeatureInfo.Title;
				feature.Description = FeatureContext.Current.FeatureInfo.Description;
				feature.Tags.AddRange(FeatureContext.Current.FeatureInfo.Tags);

				state.Report.Features.Add(feature);
				state.CurrentFeature = feature;

				RaiseEvent(ReportingFeature, state);
			}
		}

		[BeforeScenario]
		public static void BeforeScenario()
		{
			var starttime = CurrentRunTime;

			foreach (var state in reports)
			{
				var scenario = state.Factory.CreateScenario();
				scenario.StartTime = starttime;
				scenario.Title = ScenarioContext.Current.ScenarioInfo.Title;
				scenario.Tags.AddRange(ScenarioContext.Current.ScenarioInfo.Tags);

				state.CurrentFeature.Scenarios.Add(scenario);
				state.CurrentScenario = scenario;

				RaiseEvent(ReportingScenario, state);
			}
		}

		[BeforeScenarioBlock]
		public static void BeforeScenarioBlock()
		{
			var starttime = CurrentRunTime;

			foreach (var state in reports)
			{
				switch (ScenarioContext.Current.CurrentScenarioBlock)
				{
					case TechTalk.SpecFlow.ScenarioBlock.Given: state.CurrentScenarioBlock = state.CurrentScenario.Given; break;
					case TechTalk.SpecFlow.ScenarioBlock.Then: state.CurrentScenarioBlock = state.CurrentScenario.Then; break;
					case TechTalk.SpecFlow.ScenarioBlock.When: state.CurrentScenarioBlock = state.CurrentScenario.When; break;
					default:
						break;
				}

				state.CurrentScenarioBlock.StartTime = starttime;
				RaiseEvent(ReportingScenarioBlock, state);
			}
		}

		[BeforeStep]
		public static void BeforeStep()
		{
		}

		[AfterStep]
		public static void AfterStep()
		{
			TestResult testresult;
			if (ScenarioContext.Current.TestError == null)
			{
				testresult = TestResult.OK;
			}
			else
			{
				testresult = TestResult.Error;
			}

			foreach (var state in reports)
			{
				state.CurrentStep.Result = testresult;
				OnReportedStep(state);
			}
		}

		[AfterScenarioBlock]
		public static void AfterScenarioBlock()
		{
			var endtime = CurrentRunTime;
			foreach (var state in reports)
			{
				var scenarioblock = state.CurrentScenarioBlock;
				scenarioblock.EndTime = endtime;
				RaiseEvent(ReportedScenarioBlock, state);
				state.CurrentScenarioBlock = null;
			}
		}

		[AfterScenario]
		public static void AfterScenario()
		{
			foreach (var state in reports.ToArray())
			{
				var scenario = state.CurrentScenario;
				scenario.EndTime = CurrentRunTime;
				RaiseEvent(ReportedScenario, state);
				state.CurrentScenario = null;
			}
		}

		[AfterFeature]
		public static void AfterFeature()
		{
			foreach (var state in reports)
			{
				var feature = state.CurrentFeature;
				feature.EndTime = CurrentRunTime;
				RaiseEvent(ReportedFeature, state);
				state.CurrentFeature = null;
			}
		}

		[AfterTestRun]
		public static void AfterTestRun()
		{
			foreach (var state in reports)
			{
				state.Report.EndTime = CurrentRunTime;
				RaiseEvent(ReportFinished, state);
			}
		}
	}
}