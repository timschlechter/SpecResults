using System.Reflection;
using TechTalk.SpecFlow;

namespace SpecFlow.Reporting.Tests
{
	[Binding]
	public class StepDefinitions
	{
		public StepDefinitions()
		{
			// Clear report after each Scenario
			Reporter.ReportedScenario += (sender, args) =>
			{
				var writer = args.Report as ITextWriter;

				args.Report.Features.Clear();
			};
		}

		public static IReport CreateExpected(Table table)
		{
			var factory = new DefaultReportFactory();

			var report = factory.CreateReport();

			IFeature feature = null;
			IScenario scenario = null;
			IReportItem newItem = null;
			foreach (var row in table.Rows)
			{
				var type = row["Type"].ToLower();

				if (type == "feature")
				{
					feature = factory.CreateFeature();
					newItem = feature;
					report.Features.Add(feature);
				}
				else if (type == "scenario")
				{
					scenario = factory.CreateScenario();
					newItem = scenario;
					feature.Scenarios.Add(scenario);
				}
				else
				{
					var step = factory.CreateStep();
					newItem = step;
					switch (type)
					{
						case "given": scenario.Given.Steps.Add(step); break;
						case "when": scenario.When.Steps.Add(step); break;
						case "then": scenario.Then.Steps.Add(step); break;
					}
				}

				newItem.Title = row["Title"];
			}

			return report;
		}

		[Given(@"a simple SpecFlow scenario was specified")]
		public void GivenASimpleSpecFlowScenarioWasSpecified()
		{
			using (Reporter.AddStep(MethodBase.GetCurrentMethod())) { }
		}

		[When(@"I run the tests")]
		public void WhenIRunTheTests()
		{
			using (Reporter.AddStep(MethodBase.GetCurrentMethod())) { }
		}

		[Then(@"the report contains:")]
		public void ThenTheReportContains(Table table)
		{
			using (Reporter.AddStep(MethodBase.GetCurrentMethod(), table)) { }
		}

		//Reporter. += (sender, args) => { }
	}
}