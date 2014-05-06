using System.Linq;

namespace SpecFlow.Reporting
{
	public abstract class ReporterPlugin<T>
		where T : IReportingFactory, new()
	{
		public static bool Enabled
		{
			get { return Reporter.IsEnabled<T>(); }
			set { Reporter.Enable<T>(value); }
		}

		public static IReport Report
		{
			get
			{
				return Reporter.reports
					.Where(x => x.Factory is T)
					.Select(x => x.Report)
					.FirstOrDefault();
			}
		}

		protected ReporterPlugin()
		{
		}
	}
}