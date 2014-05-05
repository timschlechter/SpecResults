using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecFlow.Reporting.Json
{
	public static class JsonReporter
	{
		public static bool Enabled
		{
			get { return Reporter.IsEnabled<JsonReportingFactory>(); }
			set { Reporter.Enable<JsonReportingFactory>(value); }
		}
	}
}
