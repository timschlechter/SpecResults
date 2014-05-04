using Newtonsoft.Json;

namespace SpecFlow.Reporting.Json
{
	[JsonObject("scenario")]
	public class Scenario : TaggedReportItem, IScenario
	{
		[JsonProperty("given")]
		public IScenarioBlock Given { get; set; }

		[JsonProperty("when")]
		public IScenarioBlock When { get; set; }

		[JsonProperty("then")]
		public IScenarioBlock Then { get; set; }
	}
}