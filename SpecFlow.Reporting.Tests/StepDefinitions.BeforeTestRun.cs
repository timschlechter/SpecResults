using SpecFlow.Reporting.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using SpecFlow.Reporting.Json;

namespace SpecFlow.Reporting.Tests
{
	public partial class StepDefinitions
	{
		[BeforeTestRun]
		public static void BeforeTestRun()
		{
			Reporter.FixedRunTime = DateTime.MinValue;
			TextReporter.Enabled = true;
			JsonReporter.Enabled = true;
		}
	}
}
