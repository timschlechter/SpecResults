using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
