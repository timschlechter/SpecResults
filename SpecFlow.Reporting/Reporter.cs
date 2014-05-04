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

		private static List<ReportState> reports = new List<ReportState>();

		public static IEnumerable<IReport> Reports
		{
			get { return reports.Select(x => x.Report); }
		}

		[BeforeTestRun]
		public static void BeforeTestRun()
		{
			var starttime = DateTime.Now;

			// Create a factory instance for each non-abstract class in the current
			// appdomain which implements IReportingFactory
			var factories = AppDomain.CurrentDomain
							.GetAssemblies()
							.SelectMany(assembly => assembly.GetTypes())
							.Where(type => !type.IsInterface)
							.Where(type => !type.IsAbstract)
							.Where(type => typeof(IReportingFactory).IsAssignableFrom(type))
							.Select(type => (IReportingFactory)type.Assembly.CreateInstance(type.FullName))
							.ToList();

			// Register reporters
			reports.Clear();
			foreach (var factory in factories)
			{
				var report = factory.CreateReport();
				report.StartTime = starttime;
				reports.Add(
					new ReportState
					{
						Report = report,
						Factory = factory
					}
				);

				RaiseEvent(ReportStarted, report);
			}
		}

		[BeforeFeature]
		public static void BeforeFeature()
		{
			var starttime = DateTime.Now;

			foreach (var state in reports)
			{
				var feature = state.Factory.CreateFeature();
				feature.StartTime = starttime;
				feature.Title = FeatureContext.Current.FeatureInfo.Title;
				feature.Tags.AddRange(FeatureContext.Current.FeatureInfo.Tags);

				state.Report.Features.Add(feature);
				state.CurrentFeature = feature;

				RaiseEvent(ReportingFeature, state.Report, feature);
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

				RaiseEvent(ReportingScenario, state.Report, scenario);
			}
		}

		[BeforeScenarioBlock]
		public static void BeforeScenarioBlock()
		{
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

				RaiseEvent(ReportingScenarioBlock, state.Report, state.CurrentScenarioBlock);
			}
		}

		[BeforeStep]
		public static void BeforeStep()
		{
		}

		[AfterStep]
		public static void AfterStep()
		{
		}

		[AfterScenarioBlock]
		public static void AfterScenarioBlock()
		{
			foreach (var state in reports)
			{
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
				state.CurrentScenario = null;
			}
		}

		[AfterFeature]
		public static void AfterFeature()
		{
			foreach (var state in reports)
			{
				state.CurrentFeature.EndTime = DateTime.Now;
				state.CurrentFeature = null;
			}
		}

		[AfterTestRun]
		public static void AfterTestRun()
		{
		}

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
						foreach (var step in steps)
						{
							if (step != null)
							{
								step.EndTime = DateTime.Now;

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
	}
}