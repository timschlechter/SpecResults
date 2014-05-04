namespace SpecFlow.Reporting
{
	public interface IScenario : IReportItem, ITagged
	{
		IScenarioBlock Given { get; set; }

		IScenarioBlock When { get; set; }

		IScenarioBlock Then { get; set; }
	}
}