using System.Collections.Generic;

namespace SpecResults.Model
{
	public class Step : ReportItem
	{
		public Step()
		{
			Steps = new List<Step>();
		}

		public List<Step> Steps { get; set; }
		public TableParam Table { get; set; }
		public string MultiLineParameter { get; set; }
		public ExceptionInfo Exception { get; set; }
	}
}