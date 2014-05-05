using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TechTalk.SpecFlow;

namespace SpecFlow.Reporting
{
	[Binding]
	public partial class Reporter
	{
		#region Nested Type: ReportState

		public class ReportState
		{
			public IReport Report { get; set; }

			public IReportingFactory Factory { get; set; }

			public IFeature CurrentFeature { get; set; }

			public IScenario CurrentScenario { get; set; }

			public IScenarioBlock CurrentScenarioBlock { get; set; }

			public IStep CurrentStep { get; set; }
		}

		#endregion Nested Type: ReportState
		
		#region Factories
		
		private static List<Type> factoryTypes = new List<Type>();
		public static void Enable<T>(bool enabled)
			where T : IReportingFactory, new()
		{
			if (enabled)
			{
				factoryTypes.Add(typeof(T));
			}
			else
			{
				factoryTypes.Remove(typeof(T));
			}
		}
		public static bool IsEnabled<T>()
			where T : IReportingFactory, new()
		{
			return factoryTypes.Contains(typeof(T));
		}

		#endregion

		private static List<ReportState> reports = new List<ReportState>();

		public static IEnumerable<IReport> Reports
		{
			get { return reports.Select(x => x.Report); }
		}

		static bool testrunIsFirstFeature;
		static DateTime testrunStarttime;
		
		[BeforeTestRun]
		public static void BeforeTestRun()
		{
			testrunIsFirstFeature = true;
			testrunStarttime = DateTime.Now;			
		}

		[BeforeFeature]
		public static void BeforeFeature()
		{			
			var starttime = DateTime.Now;

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
					report.StartTime = starttime;
					var state = new ReportState
					{
						Report = report,
						Factory = factory
					};
					reports.Add(state);
					RaiseEvent(ReportStarted, state);
				}

				testrunIsFirstFeature = false ;
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
			var starttime = DateTime.Now;

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
			var starttime = DateTime.Now;

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
				testresult = TestResult.Success;
			}
			else
			{
				testresult = TestResult.Error;
			}

			foreach (var state in reports)
			{
				state.CurrentStep.Result = testresult;
			}
		}

		[AfterScenarioBlock]
		public static void AfterScenarioBlock()
		{
			var endtime = DateTime.Now;
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
				scenario.EndTime = DateTime.Now;				
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
				feature.EndTime = DateTime.Now;
				RaiseEvent(ReportedFeature, state); 
				state.CurrentFeature = null;				
			}
		}

		[AfterTestRun]
		public static void AfterTestRun()
		{
			foreach (var state in reports)
			{
				state.Report.EndTime = DateTime.Now;
				RaiseEvent(ReportFinished, state);
			}
		}

		#region AddStep
		
		public static IDisposable AddStep(MethodBase method, params object[] parameters)
		{
			var starttime = DateTime.Now;

			foreach (var state in reports)
			{
				var step = state.Factory.CreateStep();
				step.StartTime = starttime;
				step.Title = "some";

				state.CurrentScenarioBlock.Steps.Add(step);
				state.CurrentStep = step;
			}

			return new StepTitleResolver(
				method,
				reports.Select(x => x.CurrentStep),
				parameters
			);
		}

		public class StepTitleResolver : IDisposable
		{
			private bool disposed;

			public IStep[] steps { get; set; }

			public MethodBase method { get; set; }

			public StepTitleResolver(MethodBase method, IEnumerable<IStep> steps, params object[] parameters)
			{
				this.steps = steps.ToArray();
				this.method = method;
			}

			~StepTitleResolver()
			{
				Dispose(false);
			}

			public void Dispose()
			{
				Dispose(true);
			}

			private void Dispose(bool disposing)
			{
				if (!disposed)
				{
					if (disposing)
					{
						DateTime endtime = DateTime.Now;
						foreach (var step in steps)
						{
							if (step != null)
							{								
								step.EndTime = endtime;


								var attr = method.GetCustomAttributes(true).OfType<StepDefinitionBaseAttribute>().FirstOrDefault();
								if (attr != null)
								{
									step.Title = attr.Regex;
								}
							}
						}
					}
					disposed = true;
				}
			}
		}

		#endregion
	}
}