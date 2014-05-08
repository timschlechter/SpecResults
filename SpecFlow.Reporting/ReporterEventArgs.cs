using System;

namespace SpecFlow.Reporting
{
	public class ReporterEventArgs : EventArgs
	{
		public Reporter Reporter { get; internal set; }

		public Report Report { get; internal set; }

		public Feature Feature { get; internal set; }

		public Scenario Scenario { get; internal set; }

		public ScenarioBlock ScenarioBlock { get; internal set; }

		public Step Step { get; internal set; }
	}
}