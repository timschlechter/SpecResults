using System;

namespace SpecFlow.Reporting
{
	#region Nested Type: ReportEventArgs

	public class ReportEventArgs : EventArgs
	{
		public IReport Report { get; internal set; }

		public IFeature Feature { get; internal set; }

		public IScenario Scenario { get; internal set; }

		public IScenarioBlock ScenarioBlock { get; internal set; }

		public IStep Step { get; internal set; }
	}

	#endregion Nested Type: ReportEventArgs

	public partial class Reporter
	{
		public static event EventHandler<ReportEventArgs> ReportStarted;

		internal static void OnReportStarted(ReportState state)
		{
			RaiseEvent(ReportStarted, state);
		}

		public static event EventHandler<ReportEventArgs> ReportFinished;

		internal static void OnReportFinished(ReportState state)
		{
			RaiseEvent(ReportFinished, state);
		}

		public static event EventHandler<ReportEventArgs> ReportingFeature;

		internal static void OnReportingFeature(ReportState state)
		{
			RaiseEvent(ReportingFeature, state);
		}

		public static event EventHandler<ReportEventArgs> ReportedFeature;

		internal static void OnReportedFeature(ReportState state)
		{
			RaiseEvent(ReportedFeature, state);
		}

		public static event EventHandler<ReportEventArgs> ReportingScenario;

		internal static void OnReportingScenario(ReportState state)
		{
			RaiseEvent(ReportingScenario, state);
		}

		public static event EventHandler<ReportEventArgs> ReportedScenario;

		internal static void OnReportedScenario(ReportState state)
		{
			RaiseEvent(ReportedScenario, state);
		}

		public static event EventHandler<ReportEventArgs> ReportingScenarioBlock;

		internal static void OnReportingScenarioBlock(ReportState state)
		{
			RaiseEvent(ReportingScenarioBlock, state);
		}

		public static event EventHandler<ReportEventArgs> ReportedScenarioBlock;

		internal static void OnReportedScenarioBlock(ReportState state)
		{
			RaiseEvent(ReportedScenarioBlock, state);
		}

		public static event EventHandler<ReportEventArgs> ReportingStep;

		internal static void OnReportingStep(ReportState state)
		{
			RaiseEvent(ReportingStep, state);
		}

		public static event EventHandler<ReportEventArgs> ReportedStep;

		internal static void OnReportedStep(ReportState state)
		{
			RaiseEvent(ReportedStep, state);
		}

		private static void RaiseEvent(
			EventHandler<ReportEventArgs> handler, ReportState state)
		{
			if (handler != null)
			{
				handler(
					null,
					new ReportEventArgs
					{
						Report = state.Report,
						Feature = state.CurrentFeature,
						Scenario = state.CurrentScenario,
						ScenarioBlock = state.CurrentScenarioBlock,
						Step = state.CurrentStep
					}
				);
			}
		}
	}
}