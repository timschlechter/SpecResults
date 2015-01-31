using NUnit.Framework;
using SpecResults.Model;

namespace SpecResults.UnitTests.Model
{
	[TestFixture]
	public class ScenarioBlockTests
	{
		[Test]
		public void Result_WhenContainsNoSteps_ReturnsOK()
		{
			var scenarioblock = new ScenarioBlock();

			Assert.AreEqual(TestResult.OK, scenarioblock.Result);
		}

		[Test]
		public void Result_WhenContainsOKStep_ReturnsOK()
		{
			var scenarioblock = new ScenarioBlock();

			scenarioblock.Steps.Add(new Step {Result = TestResult.OK});

			Assert.AreEqual(TestResult.OK, scenarioblock.Result);
		}

		[Test]
		public void Result_WhenContainsOKAndErrorStep_ReturnsError()
		{
			var scenarioblock = new ScenarioBlock();

			scenarioblock.Steps.Add(new Step {Result = TestResult.OK});
			scenarioblock.Steps.Add(new Step {Result = TestResult.Error});

			Assert.AreEqual(TestResult.Error, scenarioblock.Result);
		}
	}
}