namespace SpecFlow.Reporting
{
	public class ScenarioBlock : Step, IScenarioBlock
	{
		public TechTalk.SpecFlow.ScenarioBlock BlockType { get; set; }

		public override TestResult Result
		{
			get
			{
				return Steps.GetResult();
			}
		}
	}
}