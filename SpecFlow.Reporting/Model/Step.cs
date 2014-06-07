using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SpecFlow.Reporting
{
	public class Step : ReportItem
	{
        public Step()
        {
            Steps = new List<Step>();
        }

		public List<Step> Steps { get; set; }

		public TableParam Table { get; set; }

        [XmlIgnore]
        public Exception Exception { get; set; }
	}
}