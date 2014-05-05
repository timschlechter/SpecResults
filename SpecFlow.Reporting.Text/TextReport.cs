using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SpecFlow.Reporting;

namespace SpecFlow.Reporting.Text
{
	public class TextReport : Report, ITextWriter
	{
		int indentSize = 4;

		public void WriteAsText(System.IO.Stream stream)
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
						foreach (var step in scenarioblock.Steps) {
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

			var bytes = Encoding.Default.GetBytes(sb.ToString());
			using (var ms = new MemoryStream(bytes))
			{
				ms.CopyTo(stream);	
			}
		}

		
	}
}
