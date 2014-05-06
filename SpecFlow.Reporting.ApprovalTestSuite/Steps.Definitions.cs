using SpecFlow.Reporting.Json;
using SpecFlow.Reporting.Text;
using System;
using TechTalk.SpecFlow;

namespace SpecFlow.Reporting.Tests
{
	[Binding]
	public partial class Steps : ReportingStepDefinitions
	{
		[BeforeTestRun]
		public static void BeforeTestRun()
		{
			Reporters.FixedRunTime = DateTime.MinValue;
			Reporters.Add(new JsonReporter());
			Reporters.Add(new PlainTextReporter());
			//Reporters.Add(new XmlReporter());

			IntializeApprovalTests();
		}

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