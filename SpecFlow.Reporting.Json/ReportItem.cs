using Newtonsoft.Json;
using System;

namespace SpecFlow.Reporting.Json
{
	[JsonObject("report_item")]
	public class ReportItem : IReportItem
	{
		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("start_time")]
		public DateTime StartTime { get; set; }

		[JsonProperty("end_time")]
		public DateTime EndTime { get; set; }

		[JsonProperty("user_data")]
		public object UserData { get; set; }

		[JsonProperty("start_timestap_utc")]
		public double StartedTimestampUTC
		{
			get
			{
				return StartTime.ToUnixTimestampUTC();
			}
		}

		[JsonProperty("end_timestap_utc")]
		public double EndTimestampUTC
		{
			get
			{
				return StartTime.ToUnixTimestampUTC();
			}
		}
	}
}