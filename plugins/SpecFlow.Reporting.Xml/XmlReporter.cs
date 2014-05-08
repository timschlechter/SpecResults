using System.Text;
using System.Xml;
namespace SpecFlow.Reporting.Xml
{
	public class XmlReporter : StringReporter
	{
		public override string Name
		{
			get { return "SpecFlow.Reporting.Xml"; }
		}

		public XmlWriterSettings Settings { get; private set; }

		public XmlReporter()
		{
			Settings = new XmlWriterSettings
			{
				Indent = true,
				IndentChars = "  ",
				Encoding = Encoding.UTF8
			};
		}

		public override void WriteToStream(System.IO.Stream stream)
		{
			using (var writer = XmlTextWriter.Create(stream, Settings))
			{
				var serializer = new System.Xml.Serialization.XmlSerializer(Report.GetType());
				serializer.Serialize(writer, Report);
			};
		}
	}
}