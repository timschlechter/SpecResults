using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
	}
}
