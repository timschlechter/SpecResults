using System;
using TechTalk.SpecFlow;
using SpecFlow.Reporting.Text;
using SpecFlow.Reporting.Json;
using SpecFlow.Reporting.Xml;
using SpecFlow.Reporting.Xml.NUnit;

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
			Reporters.Add(new XmlReporter());
			Reporters.Add(new NUnitXmlReporter());

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

		[Given(@"a ""(.*)"" scenario is specified")]
		public void GivenAScenarioIsSpecified(string p0)
		{
		}

		[When(@"the tests with ""(.*)"" parameters run ""(.*)""")]
		public void WhenTheTestsWithParametersRun(string p0, string p1)
		{
		}

		[Then(@"""(.*)"" report is generated")]
		public void ThenReportIsGenerated(int p0)
		{
		}

		[When(@"a step is not implemented")]
		public void WhenAStepIsNotImplemented()
		{
			ScenarioContext.Current.Pending();
		}
	}
}