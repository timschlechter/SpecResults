namespace SpecFlow.Reporting
{
	public interface IScenarioBlock : IStep
	{
		TechTalk.SpecFlow.ScenarioBlock BlockType { get; set; }
	}
}