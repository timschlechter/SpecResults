using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecFlow.Reporting.Text
{
	public class TextReportingFactory : ReportingFactory
	{
		public override string Name
		{
			get { return TextReporter.Name; }
		}

		protected override IReport DoCreateReport()
		{
			return new TextReport();
		}
	}
}
