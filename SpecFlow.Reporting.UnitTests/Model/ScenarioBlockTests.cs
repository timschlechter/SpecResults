using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlow.Reporting.UnitTests.Model
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

            scenarioblock.Steps.Add(new Step { Result = TestResult.OK });

            Assert.AreEqual(TestResult.OK, scenarioblock.Result);
        }

        [Test]
        public void Result_WhenContainsOKAndErrorStep_ReturnsError()
        {
            var scenarioblock = new ScenarioBlock();

            scenarioblock.Steps.Add(new Step { Result = TestResult.OK });
            scenarioblock.Steps.Add(new Step { Result = TestResult.Error });

            Assert.AreEqual(TestResult.Error, scenarioblock.Result);
        }
    }
}
