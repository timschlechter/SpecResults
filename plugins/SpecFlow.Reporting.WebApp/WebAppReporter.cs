using Newtonsoft.Json;
using SpecFlow.Reporting.Json;
using SpecFlow.Reporting.WebApp.Properties;
using System.IO;

namespace SpecFlow.Reporting.WebApp
{
	public class WebAppReporter : Reporter
	{
		protected JsonReporter JsonReporter
		{
			get
			{
				return new JsonReporter
				{
					Report = this.Report
				};
			}
		}

		public WebAppReporterSettings Settings { get; private set; }

		public WebAppReporter()
		{
			Settings = new WebAppReporterSettings();
		}

		public override void WriteToStream(Stream stream)
		{
			throw new System.NotSupportedException("The WebAppReporter only support WriteToFolder()");
		}

		public void WriteToFolder(string folderPath)
		{
			if (!Directory.Exists(folderPath))
			{
				Directory.CreateDirectory(folderPath);
			}

			// index.html
			File.WriteAllText(
				path: Path.Combine(folderPath, "index.html"),
				contents: ApplySettings(Resources.index)
			);

			// styles.css
			File.WriteAllText(
				path: Path.Combine(folderPath, "styles.css"),
				contents: Resources.styles
			);
									
			// scripts.js
			File.WriteAllText(
				path: Path.Combine(folderPath, "scripts.js"),
				contents: string.Format(
					"{0}\n{1}",
					Resources.scripts,
					Resources.templates
				)
			);

			// report.js
			File.WriteAllText(
				path: Path.Combine(folderPath, "report.js"),
				contents: string.Format("var report = {0};", JsonReporter.WriteToString())
			);
		}

		private string ApplySettings(string contents)
		{
			return contents
				.Replace("__TITLE__", Settings.GetTitle())
				.Replace("__VERSION__", Settings.GetVersion());
		}
	}
}