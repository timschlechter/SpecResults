using System;
using System.Collections.Generic;

namespace SpecFlow.Reporting
{
	public static class Extensions
	{
		public static double ToUnixTimestampUTC(this DateTime datetime)
		{
			var d1970_1_1 = new DateTime(1970, 1, 1).ToUniversalTime();

			return new TimeSpan(datetime.Ticks - d1970_1_1.Ticks).TotalMilliseconds;
		}
	}
}