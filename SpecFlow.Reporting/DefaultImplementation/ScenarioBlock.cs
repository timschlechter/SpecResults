namespace SpecFlow.Reporting
{
	public class ScenarioBlock : Step, IScenarioBlock
	{
		public TechTalk.SpecFlow.ScenarioBlock BlockType { get; set; }
	}
}