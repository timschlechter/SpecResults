namespace SpecFlow.Reporting.Xml.NUnit
{
	public class NUnitXmlReporter : XmlReporter
	{
		public NUnitXmlReporter()
		{
			XsltFile = @"SpecFlow.Reporting.Xml.NUnit.xslt";
		}
	}
}