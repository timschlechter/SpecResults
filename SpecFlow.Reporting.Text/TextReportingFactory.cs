namespace SpecFlow.Reporting.Text
{
	public class TextReportingFactory : ReportingFactory
	{
		public override string Name
		{
			get { return "SpecFlow.Reporting.Text"; }
		}

		protected override IReport DoCreateReport()
		{
			return new TextReport();
		}
	}
}