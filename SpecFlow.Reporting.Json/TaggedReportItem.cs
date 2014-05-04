using Newtonsoft.Json;
using System.Collections.Generic;

namespace SpecFlow.Reporting.Json
{
	public class TaggedReportItem : ReportItem, ITagged
	{
		[JsonProperty("tags")]
		public List<string> Tags { get; set; }
	}
}