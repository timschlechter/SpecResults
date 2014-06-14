# Generating better SpecFlow reports [![Build status](https://ci.appveyor.com/api/projects/status/yjeo6b1lonrp7jj0/branch/master)](https://ci.appveyor.com/project/TimSchlechter/specflow-reporting-196)

[SpecFlow.Reporting](https://www.nuget.org/packages/SpecFlow.Reporting) was created to get better feedback from your automated [SpecFlow](http://www.specflow.org/) testsuite. With unit tests most times reporting is only interesting for developers and testers. But when practicing BDD, the output of your automated tests might be valuable for the whole development team, management and pherhaps even end-users.

SpecFlow.Reporting makes it easy to extend SpecFlow by creating reporters which can write output in all kinds of formats and can even be enriched with additional data.

## Table of contents
  -  [Usage](#usage)
  -  [Create your own reporter](#create-your-own-reporter)
  -  [Wanted reporters](#wanted-reporters)

## Usage

Add one or more reporters to your SpecFlow project, for example:
  -  [Json](https://www.nuget.org/packages/SpecFlow.Reporting.Json/): reports in json format [example](https://raw.githubusercontent.com/specflowreporting/SpecFlow.Reporting/master/SpecFlow.Reporting.ApprovalTestSuite/approvals/SpecFlow.Reporting.Json.JsonReporter/approval.txt)
  -  [Plain Text](https://www.nuget.org/packages/SpecFlow.Reporting.Text/): reports in plain text format [example](https://raw.githubusercontent.com/specflowreporting/SpecFlow.Reporting/master/SpecFlow.Reporting.ApprovalTestSuite/approvals/SpecFlow.Reporting.Text.PlainTextReporter/approval.txt)
  -  [Xml](https://www.nuget.org/packages/SpecFlow.Reporting.Xml/): reports in xml format [example](https://raw.githubusercontent.com/specflowreporting/SpecFlow.Reporting/master/SpecFlow.Reporting.ApprovalTestSuite/approvals/SpecFlow.Reporting.Xml.XmlReporter/approval.txt)
  -  [WebApp](https://www.nuget.org/packages/SpecFlow.Reporting.WebApp/): writes an interactive, responsive, client-side web application, in which users can browse and search features, scenarios and steps [example](http://specflowreporting.azurewebsites.net/)

Work in progress:
  -  [Xml.NUnit](https://www.nuget.org/packages/SpecFlow.Reporting.Xml.NUnit/): less technical reporting in NUnit's xml output format
   
Make your existing [StepDefinitions class](https://github.com/techtalk/SpecFlow/wiki/Step-Definitions) inherit from [SpecFlow.Reporting.ReportingStepDefinitions](https://github.com/specflowreporting/SpecFlow.Reporting/blob/master/SpecFlow.Reporting/ReportingStepDefinitions.cs)

Initialize and add the reporter(s) in [BeforeTestRun] and register on one of the [events](https://github.com/specflowreporting/SpecFlow.Reporting/blob/master/SpecFlow.Reporting/Reporters.Events.cs) to get notified when something gets reported:

<pre>
[Binding]
public class StepDefinitions : ReportingStepDefinitions
{
	[BeforeTestRun]
	public static void BeforeTestRun()
	{
		Reporters.Add(new JsonReporter());
		Reporters.Add(new XmlReporter());
		Reporters.Add(new PlainTextReporter());

		Reporters.FinishedReport += (sender, args) => {
			Console.WriteLine(args.Reporter.WriteToString());
		};
	}
}	
</pre>

## Create your own reporter

Create a new project and add the [SpecFlow.Reporting package](https://www.nuget.org/packages/SpecFlow.Reporting)

Add a class which inherits from [SpecFlow.Reporting.Reporter](https://github.com/specflowreporting/SpecFlow.Reporting/blob/master/SpecFlow.Reporting/Reporter.cs) and implement the WriteToStream method:

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

## Wanted reporters
  -  Xml.MsTest
  -  Docx
  -  Xlsx
  -  ...