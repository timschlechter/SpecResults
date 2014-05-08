using System;

namespace SpecFlow.Reporting
{
	public partial class Reporters
	{
		#region Events

		public static event EventHandler<ReporterEventArgs> StartingReport;

		public static event EventHandler<ReporterEventArgs> StartingFeature;

		public static event EventHandler<ReporterEventArgs> StartingScenario;

		public static event EventHandler<ReporterEventArgs> StartingScenarioBlock;

		public static event EventHandler<ReporterEventArgs> StartingStep;

		public static event EventHandler<ReporterEventArgs> FinishedStep;

		public static event EventHandler<ReporterEventArgs> FinishedScenarioBlock;

		public static event EventHandler<ReporterEventArgs> FinishedScenario;

		public static event EventHandler<ReporterEventArgs> FinishedFeature;

		public static event EventHandler<ReporterEventArgs> FinishedReport;

		#endregion Events

		#region Event Raising

		internal static void OnReportStarted(Reporter reporter)
		{
			RaiseEvent(StartingReport, reporter);
		}

		internal static void OnStartingFeature(Reporter reporter)
		{
			RaiseEvent(StartingFeature, reporter);
		}

		internal static void OnStartingScenario(Reporter reporter)
		{
			RaiseEvent(StartingScenario, reporter);
		}

		internal static void OnStartingScenarioBlock(Reporter reporter)
		{
			RaiseEvent(StartingScenarioBlock, reporter);
		}

		internal static void OnStartingStep(Reporter reporter)
		{
			RaiseEvent(StartingStep, reporter);
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