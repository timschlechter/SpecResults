namespace SpecResults.Model
{
	public class ScenarioBlock : Step
	{
		public override TestResult Result
		{
			get { return Steps.GetResult(); }
		}
	}
}