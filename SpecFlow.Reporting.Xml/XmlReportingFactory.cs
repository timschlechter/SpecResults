namespace SpecFlow.Reporting.Xml
{
	public class XmlReportingFactory : ReportingFactory
	{
		public override string Name
		{
			get { return "SpecFlow.Reporting.Xml"; }
		}

		protected override IReport DoCreateReport()
		{
			return new XmlReport();
		}
	}
}