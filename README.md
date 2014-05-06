Usage
==================

__1. Add one or more of the reporting plugins to your SpecFlow project:__
* [SpecFlow.Reporting.Text](https://www.nuget.org/packages/SpecFlow.Reporting.Text/): generates a plain text report
* [SpecFlow.Reporting.Json](https://www.nuget.org/packages/SpecFlow.Reporting.Text/): generates a JSON report

Register the reporter in the stepflow section in your app.config:
<pre>
&lt;specFlow&gt;
	&lt;stepAssemblies&gt;
		&lt;stepAssembly assembly="SpecFlow.Reporting" /&gt;
	&lt;/stepAssemblies&gt;
&lt;/specFlow&gt;
</pre>
<em>Remark: in the near future, this step will be performed automatically when installing the NuGet package</em>

__2. Make your existing [StepDefinitions class](https://github.com/techtalk/SpecFlow/wiki/Step-Definitions) inherit from [SpecFlow.Reporting.ReportingStepDefinitions](https://github.com/TimSchlechter/SpecFlow.Reporting/blob/master/SpecFlow.Reporting/ReportingStepDefinitions.cs)__

__3. Initialize the plugin(s) in the constructor of the [StepDefinitions class](https://github.com/techtalk/SpecFlow/wiki/Step-Definitions):__
<pre>
[Binding]
public partial class StepDefinitions : ReportingStepDefinitions
{
	public StepDefinitions()
	{
		TextReporter.Enabled = true;
		JsonReporter.Enabled = true;
	}

	[Given(@"some example")]
	public void GivenSomeExample()
	{
	}
}
</pre>

__4. Register on one of the [Reporter events](https://github.com/TimSchlechter/SpecFlow.Reporting/blob/master/SpecFlow.Reporting/Reporter.Events.cs) to get notified something gets reported:__
<pre>
[Binding]
public class StepDefinitions : ReportingStepDefinitions
{
	public Steps()
	{
		TextReporter.Enabled = true;
		JsonReporter.Enabled = true;

		Reporter.ReportFinished += (sender, args) => {
			Console.WriteLine(args.Report.SerializeToString());
		};
	}

	[Given(@"some example")]
	public void GivenSomeExample()
	{
	}
}	
</pre>

