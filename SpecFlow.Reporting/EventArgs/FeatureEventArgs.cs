namespace SpecFlow.Reporting
{
	public class FeatureEventArgs : ReportEventArgs
	{
		public Feature Feature { get; internal set; }

		public FeatureEventArgs(Reporter reporter)
			: base(reporter)
		{
			Feature = Reporter.CurrentFeature;
		}
	}
}