namespace SpecFlow.Reporting.Text
{
	public class TextReporter : ReporterPlugin<TextReportingFactory>
	{
		public static string OutputFilePath { get; set; }

		protected TextReporter()
		{
		}
	}
}