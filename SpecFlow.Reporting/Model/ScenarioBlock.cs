namespace SpecFlow.Reporting
{
	public class ScenarioBlock : Step
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