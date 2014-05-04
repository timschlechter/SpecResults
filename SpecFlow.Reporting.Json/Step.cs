using Newtonsoft.Json;
using System.Collections.Generic;

namespace SpecFlow.Reporting.Json
{
	[JsonObject("step")]
	public class Step : ReportItem, IStep
	{
		[JsonProperty("steps")]
		public IList<IStep> Steps { get; set; }
	}
}