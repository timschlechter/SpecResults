using Newtonsoft.Json;

namespace SpecFlow.Reporting.Json
{
	[JsonObject("scenarioblock")]
	public class ScenarioBlock : Step, IScenarioBlock
	{
		[JsonProperty("block_type")]
		public TechTalk.SpecFlow.ScenarioBlock BlockType { get; set; }
	}
}