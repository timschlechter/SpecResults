using System;
using System.IO;
using System.Linq;
using System.Text;
using ApprovalTests.Core;
using ApprovalTests.Reporters;
using Approvals = ApprovalTests.Approvals;

namespace SpecResults.ApprovalTestSuite
{
	public partial class Steps
	{
		public static void IntializeApprovalTests()
		{
			// Clear report after each Scenario
			Reporters.FinishedReport += (sender, args) =>
			{
				var reporter = args.Reporter;

				// Replace Stack Trace value of reported exceptions, because
				// content depends on runtime environment
				var steps = reporter.Report.Features
					.SelectMany(f => f.Scenarios)
					.SelectMany(s => new[] {s.Given, s.When, s.Then})
					.SelectMany(s => s.Steps);

				foreach (var step in steps)
				{
					if (step.Exception != null)
					{
						step.Exception.StackTrace = "<removed to make tests deterministic>";
					}
				}

				// Verify IFileWriter
				var filepath = Path.GetTempFileName();
				reporter.WriteToFile(filepath);

				var received = File.ReadAllText(filepath);

				// HACK: Post process undeterministic attribute order
				received = received.Replace(" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
				received = received.Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "");

				Approvals.Verify(
					new ApprovalStringWriter(received),
					new ReportingApprovalNamer(reporter),
					new NUnitReporter()
					);
			};
		}

		#region Nested Type: ApprovalScenarioNamer

		public class ReportingApprovalNamer : IApprovalNamer
		{
			public ReportingApprovalNamer(Reporter reporter)
			{
				SourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\\..\\approvals\\", reporter.Name);
				Name = "";
			}

			public string Name { get; set; }
			public string SourcePath { get; set; }

			public static string Clean(string val)
			{
				if (string.IsNullOrEmpty(val))
				{
					return null;
				}

				return Path.GetInvalidFileNameChars().Aggregate(val, (current, c) => current.Replace(c.ToString(), string.Empty));
			}
		}

		#endregion Nested Type: ApprovalScenarioNamer

		#region Nested Type: ApprovalStringWriter

		public class ApprovalStringWriter : IApprovalWriter
		{
			public ApprovalStringWriter(string result)
			{
				Result = result;
			}

			public string Result { get; private set; }

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
				if (directory != null && !Directory.Exists(directory))
				{
					Directory.CreateDirectory(directory);
				}
				File.WriteAllText(received, Result, Encoding.UTF8);
				return received;
			}
		}

		#endregion Nested Type: ApprovalStringWriter
	}
}