using System;

namespace SpecFlow.Reporting
{
	#region EventArgs

	public abstract class IReportItemEventArgs<TItem> : EventArgs where TItem : IReportItem
	{
		public IReport Report { get; internal set; }

		public TItem Item { get; internal set; }
	}

	public class ReportEventArgs : IReportItemEventArgs<IReport> { }

	public class FeatureEventArgs : IReportItemEventArgs<IFeature> { }

	public class ScenarioEventArgs : IReportItemEventArgs<IScenario> { }

	public class ScenarioBlockEventArgs : IReportItemEventArgs<IScenarioBlock> { }

	public class StepEventArgs : IReportItemEventArgs<IStep> { }

	#endregion EventArgs

	public partial class Reporter
	{
		public static event EventHandler<ReportEventArgs> ReportStarted;

		public static event EventHandler<ReportEventArgs> ReportFinished;

		public static event EventHandler<FeatureEventArgs> ReportingFeature;

		public static event EventHandler<FeatureEventArgs> ReportedFeature;

		public static event EventHandler<ScenarioEventArgs> ReportingScenario;

		public static event EventHandler<ScenarioEventArgs> ReportedScenario;

		public static event EventHandler<ScenarioBlockEventArgs> ReportingScenarioBlock;

		public static event EventHandler<ScenarioBlockEventArgs> ReportedScenarioBlock;

		public static event EventHandler<StepEventArgs> ReportingStep;

		public static event EventHandler<StepEventArgs> ReportedStep;

		private static void RaiseEvent<TEventArgs, TReportItem>(EventHandler<TEventArgs> handler, IReport report, TReportItem item)
			where TEventArgs : IReportItemEventArgs<TReportItem>, new()
			where TReportItem : IReportItem
		{
			if (handler != null)
			{
				handler(null, new TEventArgs { Report = report, Item = item });
			}
		}

		private static void RaiseEvent(EventHandler<ReportEventArgs> handler, IReport report)
		{
			RaiseEvent<ReportEventArgs, IReport>(handler, report, report);
		}

		private static void RaiseEvent(EventHandler<FeatureEventArgs> handler, IReport report, IFeature item)
		{
			RaiseEvent<FeatureEventArgs, IFeature>(handler, report, item);
		}

		private static void RaiseEvent(EventHandler<ScenarioEventArgs> handler, IReport report, IScenario item)
		{
			RaiseEvent<ScenarioEventArgs, IScenario>(handler, report, item);
		}

		private static void RaiseEvent(EventHandler<ScenarioBlockEventArgs> handler, IReport report, IScenarioBlock item)
		{
			RaiseEvent<ScenarioBlockEventArgs, IScenarioBlock>(handler, report, item);
		}

		private static void RaiseEvent(EventHandler<StepEventArgs> handler, IReport report, IStep item)
		{
			RaiseEvent<StepEventArgs, IStep>(handler, report, item);
		}
	}
}