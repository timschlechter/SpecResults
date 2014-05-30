using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SpecFlow.Reporting
{
	public class TableParam
	{
		public List<string> Columns { get; set; }

        [XmlIgnore]
		public List<Dictionary<string, string>> Rows { get; set; }
	}
}
