using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecFlow.Reporting
{
	public class TableParam
	{
		public List<string> Columns { get; set; }

		public List<Dictionary<string, string>> Rows { get; set; }
	}
}
