namespace SpecResults
{
	public class ScenarioBlockEventArgs : ScenarioEventArgs
	{
		public ScenarioBlock ScenarioBlock { get; internal set; }

		public ScenarioBlockEventArgs(Reporter reporter)
			: base(reporter)
		{
			ScenarioBlock = reporter.CurrentScenarioBlock;
		}
	}
}