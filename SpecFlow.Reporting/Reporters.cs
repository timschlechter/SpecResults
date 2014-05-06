using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace SpecFlow.Reporting
{
	[Binding]
	public partial class Reporters
	{
		public static Reporter Add(Reporter reporter)
		{
			reporters.Add(reporter);
			return reporter;
		}

		internal static List<Reporter> reporters = new List<Reporter>();

		private static bool testrunIsFirstFeature;
		
		private static DateTime testrunStarttime;

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
				return DateTime.Now;
			}
		}
	}
}