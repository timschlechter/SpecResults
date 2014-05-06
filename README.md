Usage
==================

__1. Add one or more of the reporting plugins to your SpecFlow project:__
* [SpecFlow.Reporting.Json](https://www.nuget.org/packages/SpecFlow.Reporting.Json/): generates a Json report
* [SpecFlow.Reporting.Text](https://www.nuget.org/packages/SpecFlow.Reporting.Text/): generates a plain text report
* [SpecFlow.Reporting.Xml](https://www.nuget.org/packages/SpecFlow.Reporting.Xml/): generates a Xml report

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

__3. Initialize the plugin(s) in [BeforeTestRun]:__
<pre>
[Binding]
public partial class StepDefinitions : ReportingStepDefinitions
{
	[BeforeTestRun]
	public static void BeforeTestRun()
	{
		Reporters.Add(new JsonReporter());
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
		Reporters.Add(new JsonReporter());

		Reporter.FinishedReport += (sender, args) => {
			Console.WriteLine(args.Reporter.WriteToString());
		};
	}

	[Given(@"some example")]
	public void GivenSomeExample()
	{
	}
}	
</pre>

