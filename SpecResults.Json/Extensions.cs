using System;

namespace SpecResults.Json
{
	public static class Extensions
	{
		public static double ToUnixTimestampUtc(this DateTime datetime)
		{
			var d197011 = new DateTime(1970, 1, 1).ToUniversalTime();

			return new TimeSpan(datetime.Ticks - d197011.Ticks).TotalMilliseconds;
		}
	}
}