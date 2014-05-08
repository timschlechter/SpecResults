using System;

namespace SpecFlow.Reporting
{
	#region Nested Type: ReportEventArgs

	public class ReportEventArgs : EventArgs
	{
		public Reporter Reporter { get; internal set; }

		public Report Report { get; internal set; }

		public Feature Feature { get; internal set; }

		public Scenario Scenario { get; internal set; }

		public ScenarioBlock ScenarioBlock { get; internal set; }

		public Step Step { get; internal set; }
	}

	#endregion Nested Type: ReportEventArgs

	public partial class Reporters
	{
		#region Events

		public static event EventHandler<ReportEventArgs> StartingReport;

		public static event EventHandler<ReportEventArgs> StartingFeature;

		public static event EventHandler<ReportEventArgs> StartingScenario;

		public static event EventHandler<ReportEventArgs> StartingScenarioBlock;

		public static event EventHandler<ReportEventArgs> StartingStep;

		public static event EventHandler<ReportEventArgs> FinishedStep;

		public static event EventHandler<ReportEventArgs> FinishedScenarioBlock;

		public static event EventHandler<ReportEventArgs> FinishedScenario;

		public static event EventHandler<ReportEventArgs> FinishedFeature;

		public static event EventHandler<ReportEventArgs> FinishedReport;

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
			EventHandler<ReportEventArgs> handler, Reporter reporter)
		{
			if (handler != null)
			{
				handler(
					null,
					new ReportEventArgs
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