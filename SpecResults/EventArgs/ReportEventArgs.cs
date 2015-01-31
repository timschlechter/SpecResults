using System;

namespace SpecResults
{
	public class ReportEventArgs : EventArgs
	{
		public Reporter Reporter { get; internal set; }

		public Report Report { get; internal set; }

		public ReportEventArgs(Reporter reporter)
		{
			Reporter = reporter;
			Report = reporter.Report;
		}
	}
}