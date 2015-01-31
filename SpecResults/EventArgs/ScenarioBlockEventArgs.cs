using SpecResults.Model;

namespace SpecResults.EventArgs
{
	public class ScenarioBlockEventArgs : ScenarioEventArgs
	{
		public ScenarioBlockEventArgs(Reporter reporter)
			: base(reporter)
		{
			ScenarioBlock = reporter.CurrentScenarioBlock;
		}

		public ScenarioBlock ScenarioBlock { get; internal set; }
	}
}