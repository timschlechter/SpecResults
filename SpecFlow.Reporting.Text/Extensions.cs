using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SpecFlow.Reporting.Text
{
	public static class Extensions
	{
		public static string GetResultLabel(this IReportItem item)
		{
			return string.Format("[{0}]", item.Result.ToString());
		}

		public static string Indent(this string str, int levels = 1)
		{
			if (String.IsNullOrEmpty(str))
			{
				return str;
			}

			var lines = Regex.Split(str, Environment.NewLine);

			if (lines.Count() <= 1)
			{
				return string.Format("{0}{1}", TextReporter.IndentString, lines.FirstOrDefault());
			}

			return String.Join(Environment.NewLine, lines.Indent());
		}

		public static IEnumerable<string> Indent(this IEnumerable<string> strings, int levels = 1)
		{
			var indent = new StringBuilder(TextReporter.IndentString);
			while (levels > 1)
			{
				indent.Append(TextReporter.IndentString);
				levels--;
			}

			return strings.Select(x => (x != null ? indent + x : ""));
		}

		public static string ToPlainText(this IFeature feature)
		{
			var sb = new StringBuilder();
			sb.AppendLine("Feature: " + feature.Title + " " + feature.GetResultLabel());

			if (!String.IsNullOrEmpty(feature.Description))
			{
				sb.AppendLine(feature.Description.Indent());
			}

			sb.AppendLine();

			foreach (var scenario in feature.Scenarios)
			{
				sb.AppendLine(scenario.ToPlainText());
			}

			return sb.ToString();
		}

		public static string ToPlainText(this IScenario scenario)
		{
			var sb = new StringBuilder();
			sb.AppendLine("Scenario: " + scenario.Title + " " + scenario.GetResultLabel());

			foreach (var scenarioblock in scenario.GetBlocks())
			{
				sb.Append(scenarioblock.ToPlainText());
			}

			return sb.ToString();
		}

		public static string ToPlainText(this IScenarioBlock scenarioblock)
		{
			var sb = new StringBuilder();

			foreach (var step in scenarioblock.Steps)
			{
				sb.AppendLine(step.ToPlainText(sb.Length == 0 ? scenarioblock.BlockType.ToString() : "And").Indent());
			}

			return sb.ToString();
		}

		public static string ToPlainText(this IStep step, string prefix)
		{
			return string.Format(
				"{0} {1} {2}",
				prefix,
				step.Title,
				step.GetResultLabel()
			);
		}
	}
}