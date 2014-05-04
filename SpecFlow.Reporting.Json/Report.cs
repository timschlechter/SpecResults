using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace SpecFlow.Reporting.Json
{
	[JsonObject("report")]
	public class Report : ReportItem, IReport, ITextWriter
	{
		[JsonProperty("features")]
		public List<IFeature> Features { get; set; }

		public virtual void WriteAsText(Stream stream)
		{
			throw new System.NotImplementedException();
		}
	}
}