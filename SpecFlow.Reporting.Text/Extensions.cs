using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SpecFlow.Reporting.Text
{
	public static class Extensions
	{
		public static string GetResultLabel(this IReportItem item)
		{
			return string.Format("[{0}]", item.Result.ToString());
		}

		public static string Indent(this string str, int count)
		{
			if (String.IsNullOrEmpty(str))
			{
				return str;
			}

			var lines = Regex.Split(str, Environment.NewLine);

			return String.Join(Environment.NewLine, lines.Indent(4));
		}

		public static IEnumerable<string> Indent(this IEnumerable<string> strings, int count)
		{
			return strings.Select(x => "".PadLeft(count) + (x != null ? x : ""));
		}
	}
}