using ApprovalTests.Core;
using ApprovalTests.Reporters;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace SpecFlow.Reporting.Tests
{
	public partial class Steps
	{
		#region Nested Type: ApprovalScenarioNamer

		public class ReportingApprovalNamer : IApprovalNamer
		{
			private static string Clean(string val)
			{
				if (string.IsNullOrEmpty(val))
				{
					return null;
				}

				return Path.GetInvalidFileNameChars().Aggregate(val, (current, c) => current.Replace(c.ToString(), string.Empty));
			}

			public ReportingApprovalNamer(Reporter reporter, string name)
			{
				SourcePath = Path.Combine(@"..\\..\\approvals\\", reporter.Name);
				Name = name;
			}

			public string Name
			{
				get;
				set;
			}

			public string SourcePath
			{
				get;
				set;
			}
		}

		#endregion Nested Type: ApprovalScenarioNamer

		#region Nested Type: ApprovalStringWriter

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

		#endregion Nested Type: ApprovalStringWriter

		private static void IntializeApprovalTests()
		{
			// Clear report after each Scenario
			Reporters.FinishedReport += (sender, args) =>
			{
				var reporter = args.Reporter;

				// Verify ISteamWriter
				var serialized = reporter.WriteToString();
				Verify(serialized, reporter, "WriteToString");

				// Verify IFileWriter
				var filepath = Path.GetTempFileName();
				reporter.WriteToFile(filepath);
				Console.WriteLine("Verify " + filepath);
				Verify(File.ReadAllText(filepath), reporter, "WriteToFile");
			};
		}

		private static void Verify(string result, Reporter reporter, string testname)
		{
			ApprovalTests.Approvals.Verify(
				new ApprovalStringWriter(result),
				new ReportingApprovalNamer(reporter, testname),
				new BeyondCompareReporter()
			);
		}
	}
}