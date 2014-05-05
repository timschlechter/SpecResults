namespace SpecFlow.Reporting
{
	public interface IReportingFactory
	{
		string Name { get; }

		IReport CreateReport();

		IFeature CreateFeature();

		IScenario CreateScenario();

		IScenarioBlock CreateScenarioBlock();

		IStep CreateStep();
	}
}