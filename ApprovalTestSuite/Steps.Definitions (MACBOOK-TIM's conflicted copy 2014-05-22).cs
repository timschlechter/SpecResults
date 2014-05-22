using SpecFlow.Reporting.Json;
using SpecFlow.Reporting.Text;
using SpecFlow.Reporting.Xml;
using SpecFlow.Reporting.Xml.NUnit;
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

		[When]
		public void When_a_step_uses_method_name_style()
		{
		}

		[When]
		public void When_a_step_uses_method_name_style_with_a_P0(string p0)
		{			
		}
		[When]
		public void When_a_step_uses_P0_with_P1_parameters(string p0, string p1)
		{
			ScenarioContext.Current.Pending();
		}

		[When(@"the a step contains a table:")]
		public void WhenTheAStepContainsATable(TableParam table)
		{
		}

	}
}