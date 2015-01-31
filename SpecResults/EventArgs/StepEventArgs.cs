using SpecResults.Model;

namespace SpecResults.EventArgs
{
	public class StepEventArgs : ScenarioBlockEventArgs
	{
		public StepEventArgs(Reporter reporter)
			: base(reporter)
		{
			Step = reporter.CurrentStep;
		}

		public Step Step { get; internal set; }
	}
}