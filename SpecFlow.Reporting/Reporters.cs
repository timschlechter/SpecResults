using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace SpecFlow.Reporting
{
    [Binding]
    public static partial class Reporters
    {
        private static List<Reporter> reporters = new List<Reporter>();        

        /// <summary>
        /// Set fixed start and end times. Usefull for automated tests.
        /// </summary>
        public static DateTime? FixedRunTime
        {
            get;
            set;
        }

        /// <summary>
        /// Returns the current date/time which is used during the test run. It can set to a fixed
        /// datetime by <see cref="FixedRunTime" />
        /// </summary>
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

        public static Reporter Add(Reporter reporter)
        {
            reporters.Add(reporter);
            return reporter;
        }

        public static IEnumerable<Reporter> GetAll()
        {
            return reporters;
        }
    }
}