using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace SpecFlow.Reporting
{
	public static class Extensions
	{
		private static string CleanFileName(string fileName)
		{
			return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));
		}

		public static TestResult GetResult(this IEnumerable<IReportItem> items)
		{
			return items.Select(x => x.Result).GetResult();
		}

		public static TestResult GetResult(this IEnumerable<TestResult> results)
		{
			if (results.Any(x => x == TestResult.Error))
			{
				return TestResult.Error;
			}

			if (results.Any(x => x == TestResult.NotImplemented))
			{
				return TestResult.NotImplemented;
			}

			if (results.Any(x => x == TestResult.NotRun))
			{
				return TestResult.NotRun;
			}			

			if (results.Any(x => x == TestResult.Unknown))
			{
				return TestResult.Unknown;
			}			

			return TestResult.Success;
		}

		public static IEnumerable<IScenarioBlock> GetBlocks(this IScenario scenario)
		{
			return new[] { scenario.Given, scenario.When, scenario.Then };
		}
	}
}