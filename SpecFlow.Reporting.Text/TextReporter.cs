namespace SpecFlow.Reporting.Text
{
	public class TextReporter : ReporterPlugin<TextReportingFactory>
	{
		public static string IndentString { get; set; }
		public static bool WriteToConsole { get; set; }

		static TextReporter()
		{
			IndentString = "    ";
		}

		protected TextReporter()
		{
			
		}
	}
}