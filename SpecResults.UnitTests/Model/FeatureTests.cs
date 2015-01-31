using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecResults.UnitTests.Model
{
    [TestFixture]
    public class FeatureTests
    {
        [Test]
        [TestCase("line 1\nline 2", "line 1\nline 2")]
        [TestCase("line 1\r\nline 2", "line 1\nline 2")]
        public void Description_SetWithCRandLF_OnlyContainsLR(string description, string expectedResult)
        {
            var feature = new Feature { Description = description };

            Assert.AreEqual(expectedResult, feature.Description);
        }

        [Test]
        [TestCase("# Header", "<h1>Header</h1>")]
        [TestCase("Header\n======", "<h1>Header</h1>")]
        [TestCase("line 1\nline 2", "<p>line 1<br />line 2</p>")]
        [TestCase("line 1\r\nline 2", "<p>line 1<br />line 2</p>")]
		[TestCase("* Header", "<ul><li>Header</li></ul>")]
        public void DescriptionHtml_TestCases(string description, string expectedResult)
        {
            var feature = new Feature { Description = description };

            Assert.AreEqual(expectedResult, feature.DescriptionHtml);
        }
    }
}
