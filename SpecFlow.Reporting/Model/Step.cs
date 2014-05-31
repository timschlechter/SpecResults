using System.Collections.Generic;

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
	}
}