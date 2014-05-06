using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SpecFlow.Reporting.Xml
{
	[Serializable]
	public class XmlReport : Report, IStreamWriter
	{
		public void Write(Stream stream)
		{
			throw new NotImplementedException();
			//var x = new XmlSerializer(this.GetType());
			//x.Serialize(stream, this);
		}
	}
}