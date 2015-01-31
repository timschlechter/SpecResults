namespace SpecResults
{
	public class StepEventArgs : ScenarioBlockEventArgs
	{
		public Step Step { get; internal set; }

		public StepEventArgs(Reporter reporter)
			: base(reporter)
		{
			Step = reporter.CurrentStep;
		}
	}
}