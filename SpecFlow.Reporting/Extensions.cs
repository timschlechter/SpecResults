using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

		public static string SerializeToString(this IReport report)
		{
			var streamwriter = report as IStreamWriter;

			if (streamwriter == null)
			{
				throw new NotImplementedException("The report does not implement SpecFlow.Reporting.IStreamWriter");
			}

			using (var stream = new MemoryStream())
			{
				streamwriter.Write(stream);
				stream.Position = 0;
				using (var reader = new StreamReader(stream))
				{
					return reader.ReadToEnd();
				}
			}
		}

		public static void WriteToFile(this IReport report, string filepath)
		{
			var filewriter = report as IFileWriter;

			if (filewriter == null)
			{
				throw new NotImplementedException("The report does not implement SpecFlow.Reporting.IFileWriter");
			}

			filewriter.WriteFile(filepath);
		}
	}
}