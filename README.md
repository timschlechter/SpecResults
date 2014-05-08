# SpecFlow.Reporting

 - [Usage](#usage)
 - [Creating your own reporters](#creating-your-own-reporters)

## Usage

Add one or more of the reporting plugins to your SpecFlow project:
 - [Json](https://www.nuget.org/packages/SpecFlow.Reporting.Json/): reports in json format
 - [Text](https://www.nuget.org/packages/SpecFlow.Reporting.Text/): reports in plain text format
 - [Xml](https://www.nuget.org/packages/SpecFlow.Reporting.Xml/): reports in xml format
 - [Xml.NUnit](https://www.nuget.org/packages/SpecFlow.Reporting.Xml.NUnit/): reports in NUnit's output xml format

Register the reporter in the stepflow section in your app.config:
<pre>
&lt;specFlow&gt;
	&lt;stepAssemblies&gt;
		&lt;stepAssembly assembly="SpecFlow.Reporting" /&gt;
	&lt;/stepAssemblies&gt;
&lt;/specFlow&gt;
</pre>
<em>Remark: in the near future, this step will be performed automatically when installing the NuGet package</em>

Make your existing [StepDefinitions class](https://github.com/techtalk/SpecFlow/wiki/Step-Definitions) inherit from [SpecFlow.Reporting.ReportingStepDefinitions](https://github.com/TimSchlechter/SpecFlow.Reporting/blob/master/SpecFlow.Reporting/ReportingStepDefinitions.cs)__

Initialize the plugin(s) in [BeforeTestRun]:
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

Register on one of the [Reporter events](https://github.com/TimSchlechter/SpecFlow.Reporting/blob/master/SpecFlow.Reporting/Reporter.Events.cs) to get notified something gets reported:
<pre>
[Binding]
public class StepDefinitions : ReportingStepDefinitions
{
	public Steps()
	{
		Reporters.Add(new JsonReporter());

		Reporters.FinishedReport += (sender, args) => {
			Console.WriteLine(args.Reporter.WriteToString());
		};
	}

	[Given(@"some example")]
	public void GivenSomeExample()
	{
	}
}	
</pre>

## Creating your own reporters

Create a new project and add the [SpecFlow.Reporting package](https://www.nuget.org/packages/SpecFlow.Reporting)

Add a class which inherits from [SpecFlow.Reporting.Reporter](https://github.com/TimSchlechter/SpecFlow.Reporting/blob/master/SpecFlow.Reporting/Reporter.cs) and implement the WriteToStream method:

<pre>
namespace SpecFlow.Reporting.MyFormat
{
	public class MyFormatReporter : SpecFlow.Reporting.Reporter
	{
		public override void WriteToStream(Stream stream)
		{
			//
			// TODO: Serialize this.Report to the stream
			//
		}
	}
}
</pre>