using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecFlow.Reporting.Text
{
	public static class TextReporter
	{
		internal static string Name { get { return "SpecFlow.Reporting.Text"; } }

		public static string OutputFilePath { get; set; }
		public static bool Enabled
		{
			get { return Reporter.IsEnabled<TextReportingFactory>(); }
			set { Reporter.Enable<TextReportingFactory>(value); }
		}

		public static IReport Report
		{
			get	{ return Reporter.Reports.FirstOrDefault(x => x.Generator == Name);	}
		}		
	}
}
