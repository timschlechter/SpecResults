using System;

namespace SpecFlow.Reporting.WebApp
{
	public class WebAppReporterSettings
	{
		public string Title { get; set; }

		internal string GetTitle()
		{
			return String.IsNullOrEmpty(Title) ? "{WebAppReporter.Settings.Title}" : Title;
		}

		internal string GetVersion()
		{
			return Guid.NewGuid().ToString();
		}

		public string StepDetailsTemplateFile { get; set; }

		public string CssFile { get; set; }

		public string DashboardTextFile { get; set; }
	}
}