using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecFlow.Reporting.Text
{
	public static class TextReporter
	{
		public static string OutputFilePath { get; set; }
		public static bool Enabled
		{
			get { return Reporter.IsEnabled<TextReportingFactory>(); }
			set { Reporter.Enable<TextReportingFactory>(value); }
		}
	}
}
