using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using TechTalk.SpecFlow;

namespace SpecFlow.Reporting
{
	[Binding]
	public partial class Reporter
	{
		#region Nested Type: ReportState

		public class ReportState
		{
			public IReport Report { get; set; }

			public IReportingFactory Factory { get; set; }

			public IFeature CurrentFeature { get; set; }

			public IScenario CurrentScenario { get; set; }

			public IScenarioBlock CurrentScenarioBlock { get; set; }

			public IStep CurrentStep { get; set; }
		}

		#endregion Nested Type: ReportState

		#region Factories

		private static List<Type> factoryTypes = new List<Type>();
		public static void Enable<T>(bool enabled)
			where T : IReportingFactory, new()
		{
			if (enabled)
			{
				factoryTypes.Add(typeof(T));
			}
			else
			{
				factoryTypes.Remove(typeof(T));
			}
		}
		public static bool IsEnabled<T>()
			where T : IReportingFactory, new()
		{
			return factoryTypes.Contains(typeof(T));
		}

		#endregion

		internal static List<ReportState> reports = new List<ReportState>();

		public static IEnumerable<IReport> Reports
		{
			get { return reports.Select(x => x.Report); }
		}

		static bool testrunIsFirstFeature;

		/// <summary>
		/// Set fixed start and end times. Usefull for automated tests.
		/// </summary>
		public static DateTime? FixedRunTime
		{
			get;
			set;
		}

		internal static DateTime CurrentRunTime
		{
			get
			{
				if (FixedRunTime.HasValue)
				{
					return FixedRunTime.Value;
				}
				return CurrentRunTime;
			}
		}

		static DateTime testrunStarttime;
	}
}
