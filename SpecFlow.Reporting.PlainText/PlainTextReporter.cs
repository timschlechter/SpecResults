using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SpecFlow.Reporting.Text
{
    public class PlainTextReporter : Reporter
    {
        public string DefaultFileName { get { return "testresults.txt"; } }

        public string IndentString { get; set; }

        public bool WriteToConsole { get; set; }

        public PlainTextReporter()
        {
            IndentString = "    ";
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

        public static string GetResultLabel(ReportItem item)
        {
            return string.Format("[{0}] in {1}ms", item.Result.ToString(), (item.EndTime - item.StartTime).Milliseconds);
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

            sb.Append(ToPlainText(TechTalk.SpecFlow.ScenarioBlock.Given, scenario.Given));
            sb.Append(ToPlainText(TechTalk.SpecFlow.ScenarioBlock.When, scenario.When));
            sb.Append(ToPlainText(TechTalk.SpecFlow.ScenarioBlock.Then, scenario.Then));

            return sb.ToString();
        }

        public string ToPlainText(TechTalk.SpecFlow.ScenarioBlock blockType, ScenarioBlock scenarioblock)
        {
            var sb = new StringBuilder();

            foreach (var step in scenarioblock.Steps)
            {
                var text = ToPlainText(step, sb.Length == 0 ? blockType.ToString() : "And");
                sb.AppendLine(Indent(text));
            }

            return sb.ToString();
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
    }
}