using ApprovalTests;
using ApprovalTests.Core;
using ApprovalTests.Reporters;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Reflection;
using TechTalk.SpecFlow;
using System.Text;
using ApprovalTests.Writers;
using System;

namespace SpecFlow.Reporting.Tests
{
	[Binding]
	public partial class StepDefinitions : ReportingStepDefinitions
	{
		#region Approval Testing Helpers
		
		public class ApprovalScenarioNamer : IApprovalNamer
		{

			private static string Clean(string val) {
				if (string.IsNullOrEmpty(val))
				{
					return null;
				}

				return Path.GetInvalidFileNameChars().Aggregate(val, (current, c) => current.Replace(c.ToString(), string.Empty));
			}
			public ApprovalScenarioNamer(IReport report, IFeature feature, IScenario scenario, string name)
			{
				SourcePath = string.Format(@"..\\..\\Approvals\\{0}\Features\{1}\Scenarios\{2}", Clean(report.Generator), Clean(feature.Title), Clean(scenario.Title));

				Name = name;
			}
			public string Name
			{
				get;
				set;
			}

			public string SourcePath
			{
				get;set;
			}
		}

		public class ApprovalStringWriter : IApprovalWriter
		{
			public string Result { get; private set; }

			public ApprovalStringWriter(string result)
			{
				this.Result = result;
			}

			public string GetApprovalFilename(string basename)
			{
				return Path.Combine(basename, "approval.txt");
			}

			public string GetReceivedFilename(string basename)
			{
				return Path.Combine(basename, "received.txt");
			}

			public string WriteReceivedFile(string received)
			{
				var directory = Path.GetDirectoryName(received);
				if (!Directory.Exists(directory))
				{
					Directory.CreateDirectory(directory);
				}
				File.WriteAllText(received, Result, Encoding.UTF8);
				return received;
			}
		}

		#endregion

		static StepDefinitions()
		{
			
			// Clear report after each Scenario
			Reporter.ReportedScenario += (sender, args) =>
			{
				var reporter = new BeyondCompareReporter();

				// Verify ISteamWriter
				var streamWriter = args.Report as IStreamWriter;
				if (streamWriter != null)
				{
					using (var stream = new MemoryStream())
					{
						streamWriter.Write(stream);
						stream.Position = 0;
						using (var reader = new StreamReader(stream))
						{
							var result = reader.ReadToEnd();

							ApprovalTests.Approvals.Verify(
								new ApprovalStringWriter(result),
								new ApprovalScenarioNamer(args.Report, args.Feature, args.Scenario, "IStreamWriter"),
								reporter
							);
						}
					}
				}

				// Verify IFileWriter
				var fileWriter = args.Report as IFileWriter;
				if (fileWriter != null)
				{
					var filepath = Path.GetTempFileName();
					fileWriter.WriteFile(filepath);
					using (var stream = new MemoryStream())
					{
						streamWriter.Write(stream);
						stream.Position = 0;
						using (var reader = new StreamReader(stream))
						{
							var result = reader.ReadToEnd();

							ApprovalTests.Approvals.Verify(								
								new ApprovalStringWriter(File.ReadAllText(filepath)),
								new ApprovalScenarioNamer(args.Report, args.Feature, args.Scenario, "IFileWriter"),								
								reporter
							);
						}
					}
				}

				args.Report.Features.Clear();
			};
		}

		[Given(@"a scenario is specified")]
		public void GivenAScenarioIsSpecified()
		{			
		}

		[When(@"the tests run")]
		public void WhenTheTestsRun()
		{
		}

		[Then(@"a report is generated")]
		public void ThenAReportIsGenerated()
		{
		}

	}
}