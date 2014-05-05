using ApprovalTests;
using ApprovalTests.Core;
using ApprovalTests.Reporters;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Reflection;
using TechTalk.SpecFlow;
using System.Text;
using ApprovalTests.Writers;
using System;

namespace SpecFlow.Reporting.Tests
{
	[Binding]
	public partial class Steps : ReportingStepDefinitions
	{
		[Given(@"a scenario is specified")]
		public void GivenAScenarioIsSpecified()
		{			
		}

		[When(@"the tests run")]
		public void WhenTheTestsRun()
		{
		}

		[Then(@"a report is generated")]
		public void ThenAReportIsGenerated()
		{
		}

	}
}