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

	#endregion EventArgs

	public partial class Reporter
	{
		public static event EventHandler<ReportEventArgs> ReportStarted;

		public static event EventHandler<ReportEventArgs> ReportFinished;

		public static event EventHandler<ReportEventArgs> ReportingFeature;

		public static event EventHandler<ReportEventArgs> ReportedFeature;

		public static event EventHandler<ReportEventArgs> ReportingScenario;

		public static event EventHandler<ReportEventArgs> ReportedScenario;

		public static event EventHandler<ReportEventArgs> ReportingScenarioBlock;

		public static event EventHandler<ReportEventArgs> ReportedScenarioBlock;

		public static event EventHandler<ReportEventArgs> ReportingStep;

		public static event EventHandler<ReportEventArgs> ReportedStep;

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