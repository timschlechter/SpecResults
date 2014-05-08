using System;

namespace SpecFlow.Reporting
{
	public partial class Reporters
	{
		#region Events

		public static event EventHandler<ReporterEventArgs> StartedReport;

		public static event EventHandler<ReporterEventArgs> StartedFeature;

		public static event EventHandler<ReporterEventArgs> StartedScenario;

		public static event EventHandler<ReporterEventArgs> StartedScenarioBlock;

		public static event EventHandler<ReporterEventArgs> StartedStep;

		public static event EventHandler<ReporterEventArgs> FinishedStep;

		public static event EventHandler<ReporterEventArgs> FinishedScenarioBlock;

		public static event EventHandler<ReporterEventArgs> FinishedScenario;

		public static event EventHandler<ReporterEventArgs> FinishedFeature;

		public static event EventHandler<ReporterEventArgs> FinishedReport;

		#endregion Events

		#region Event Raising

		internal static void OnReportStarted(Reporter reporter)
		{
			RaiseEvent(StartedReport, reporter);
		}

		internal static void OnStartedFeature(Reporter reporter)
		{
			RaiseEvent(StartedFeature, reporter);
		}

		internal static void OnStartedScenario(Reporter reporter)
		{
			RaiseEvent(StartedScenario, reporter);
		}

		internal static void OnStartedScenarioBlock(Reporter reporter)
		{
			RaiseEvent(StartedScenarioBlock, reporter);
		}

		internal static void OnStartedStep(Reporter reporter)
		{
			RaiseEvent(StartedStep, reporter);
		}

		internal static void OnFinishedStep(Reporter reporter)
		{
			RaiseEvent(FinishedStep, reporter);
		}

		internal static void OnFinishedScenarioBlock(Reporter reporter)
		{
			RaiseEvent(FinishedScenarioBlock, reporter);
		}

		internal static void OnFinishedScenario(Reporter reporter)
		{
			RaiseEvent(FinishedScenario, reporter);
		}

		internal static void OnFinishedFeature(Reporter reporter)
		{
			RaiseEvent(FinishedFeature, reporter);
		}

		internal static void OnFinishedReport(Reporter reporter)
		{
			RaiseEvent(FinishedReport, reporter);
		}

		private static void RaiseEvent(
			EventHandler<ReporterEventArgs> handler, Reporter reporter)
		{
			if (handler != null)
			{
				handler(
					null,
					new ReporterEventArgs
					{
						Reporter = reporter,
						Report = reporter.Report,
						Feature = reporter.CurrentFeature,
						Scenario = reporter.CurrentScenario,
						ScenarioBlock = reporter.CurrentScenarioBlock,
						Step = reporter.CurrentStep
					}
				);
			}
		}

		#endregion Event Raising
	}
}