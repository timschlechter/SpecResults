using System;
using System.IO;
using System.Text;

namespace SpecFlow.Reporting.Text
{
	public class TextReport : Report, IStreamWriter, IFileWriter
	{
		private int indentSize = 4;

		public void Write(Stream stream)
		{
			var sb = new StringBuilder();
			foreach (var feature in Features)
			{
				sb.AppendLine(feature.GetResultLabel() + " Feature: " + feature.Title);
				if (!String.IsNullOrEmpty(feature.Description))
				{
					sb.AppendLine(feature.Description.Indent(indentSize));
				}
				sb.AppendLine();

				foreach (var scenario in feature.Scenarios)
				{
					sb.AppendLine(scenario.GetResultLabel() + " Scenario: " + scenario.Title);

					foreach (var scenarioblock in scenario.GetBlocks())
					{
						bool firstStep = true;
						foreach (var step in scenarioblock.Steps)
						{
							sb.AppendLine(
								string.Format(
								"{0} {1} {2} {3}",
								step.GetResultLabel(),
								"".Indent(indentSize),
								firstStep ? scenarioblock.BlockType.ToString() : "And",
								step.Title
							));
							if (firstStep)
							{
								firstStep = false;
							}
						}
					}
				}
			}

			var bytes = Encoding.UTF8.GetBytes(sb.ToString());
			using (var ms = new MemoryStream(bytes))
			{
				ms.CopyTo(stream);
			}
		}

		public void WriteFile(string filepath)
		{
			using (var ms = new MemoryStream())
			{
				Write(ms);
				ms.Seek(0, SeekOrigin.Begin);
				using (var fs = File.Create(filepath))
				{
					ms.CopyTo(fs);
				}
			}
		}
	}
}