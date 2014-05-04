namespace SpecFlow.Reporting
{
	public interface IReportingFactory
	{
		IReport CreateReport();

		IFeature CreateFeature();

		IScenario CreateScenario();

		IScenarioBlock CreateScenarioBlock();

		IStep CreateStep();
	}
}