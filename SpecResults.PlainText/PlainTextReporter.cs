using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SpecResults.Model;

namespace SpecResults.PlainText
{
	public class PlainTextReporter : Reporter
	{
		public PlainTextReporter()
		{
			IndentString = "    ";
		}

		public string DefaultFileName
		{
			get { return "testresults.txt"; }
		}

		public string IndentString { get; set; }
		public bool WriteToConsole { get; set; }

		public static string GetResultLabel(ReportItem item)
		{
			return string.Format("[{0}] in {1}ms", item.Result, (item.EndTime - item.StartTime).Milliseconds);
		}

		public string Indent(string str, int levels = 1)
		{
			if (String.IsNullOrEmpty(str))
			{
				return str;
			}

			var lines = str.Split('\n');

			if (lines.Count() <= 1)
			{
				return string.Format("{0}{1}", IndentString, lines.FirstOrDefault());
			}

			return String.Join(Environment.NewLine, Indent(lines));
		}

		public IEnumerable<string> Indent(IEnumerable<string> strings, int levels = 1)
		{
			var indent = new StringBuilder(IndentString);
			while (levels > 1)
			{
				indent.Append(IndentString);
				levels--;
			}

			return strings.Select(x => (x != null ? indent + x : ""));
		}

		public string ToPlainText(Feature feature)
		{
			var sb = new StringBuilder();
			sb.AppendLine("Feature: " + feature.Title + " " + GetResultLabel(feature));

			if (!String.IsNullOrEmpty(feature.Description))
			{
				sb.AppendLine(Indent(feature.Description));
			}

			foreach (var scenario in feature.Scenarios)
			{
				sb.AppendLine();
				sb.Append(ToPlainText(scenario));
			}

			return sb.ToString();
		}

		public string ToPlainText(Scenario scenario)
		{
			var sb = new StringBuilder();
			sb.AppendLine("Scenario: " + scenario.Title + " " + GetResultLabel(scenario));

			if (scenario.Given.Steps.Any())
			{
				sb.AppendLine(ToPlainText(TechTalk.SpecFlow.ScenarioBlock.Given, scenario.Given));
			}

			if (scenario.When.Steps.Any())
			{
				sb.AppendLine(ToPlainText(TechTalk.SpecFlow.ScenarioBlock.When, scenario.When));
			}

			if (scenario.Then.Steps.Any())
			{
				sb.AppendLine(ToPlainText(TechTalk.SpecFlow.ScenarioBlock.Then, scenario.Then));
			}

			return sb.ToString();
		}

		public string ToPlainText(TechTalk.SpecFlow.ScenarioBlock blockType, ScenarioBlock scenarioblock)
		{
			return ToPlainText(scenarioblock.Steps, blockType.ToString());
		}

		public string ToPlainText(IEnumerable<Step> steps, string prefix)
		{
			var sb = new StringBuilder();
			var stepsArray = steps.ToArray();
			for (var i = 0; i < stepsArray.Count(); i++)
			{
				if (i > 0)
				{
					sb.AppendLine();
				}
				var step = stepsArray[i];
				sb.Append(ToPlainText(step, i == 0 ? prefix : "And"));
				if (step.Table != null)
				{
					sb.AppendLine();
					sb.Append(ToPlainText(step.Table));
				}
				if (step.MultiLineParameter != null)
				{
					sb.AppendLine();
					sb.AppendLine("\"\"\"");
					sb.Append(step.MultiLineParameter);
					sb.AppendLine();
					sb.Append("\"\"\"");
				}
				if (step.Steps.Any())
				{
					sb.AppendLine();
					sb.Append(ToPlainText(step.Steps, prefix));
					if (step.Table != null)
					{
						sb.AppendLine();
						sb.Append(ToPlainText(step.Table));
					}
					if (step.MultiLineParameter != null)
					{
						sb.AppendLine();
						sb.AppendLine("\"\"\"");
						sb.Append(step.MultiLineParameter);
						sb.AppendLine("\"\"\"");
					}
				}
			}

			return Indent(sb.ToString());
		}

		public string ToPlainText(Step step, string prefix)
		{
			return string.Format(
				"{0} {1} {2}",
				prefix,
				step.Title,
				GetResultLabel(step)
				);
		}

		public string ToPlainText(TableParam table)
		{
			var rows = new StringBuilder();
			var columnsRow = new StringBuilder("|");
			for (var i = 0; i < table.Columns.Count; i++)
			{
				var width = table.GetMaxColumnCharacters(i);
				columnsRow.Append(" " + table.Columns[i].PadRight(width, ' ') + " |");
			}
			rows.Append(columnsRow);
			foreach (var row in table.Rows)
			{
				rows.AppendLine();
				var rowString = new StringBuilder("|");
				var index = 0;
				foreach (var value in row.Values)
				{
					var width = table.GetMaxColumnCharacters(index);
					rowString.Append(" " + value.PadRight(width, ' ') + " |");
					index++;
				}
				rows.Append(rowString);
			}
			return rows.ToString();
		}

		public override void WriteToStream(Stream stream)
		{
			var sb = new StringBuilder();
			foreach (var feature in Report.Features)
			{
				sb.AppendLine(ToPlainText(feature));
			}

			var bytes = Encoding.UTF8.GetBytes(sb.ToString());
			using (var ms = new MemoryStream(bytes))
			{
				ms.CopyTo(stream);
			}
		}
	}
}