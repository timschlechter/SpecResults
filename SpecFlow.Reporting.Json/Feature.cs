using Newtonsoft.Json;
using System.Collections.Generic;

namespace SpecFlow.Reporting.Json
{
	[JsonObject("feature")]
	public class Feature : TaggedReportItem, IFeature
	{
		[JsonProperty("scenarios")]
		public List<IScenario> Scenarios { get; set; }

		public void AddScenario(IScenario scenario)
		{
			Scenarios.Add(scenario);
		}
	}
}