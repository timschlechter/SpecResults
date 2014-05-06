namespace SpecFlow.Reporting.Xml
{
	public class XmlReporter : StringReporter
	{
		public override string Name
		{
			get { return "SpecFlow.Reporting.Xml"; }
		}

		public override void WriteToStream(System.IO.Stream stream)
		{
			throw new System.NotImplementedException();
		}
	}
}