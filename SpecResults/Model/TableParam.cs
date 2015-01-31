using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SpecResults
{
	public class TableParam
	{
		public List<string> Columns { get; set; }

        [XmlIgnore]
		public List<Dictionary<string, string>> Rows { get; set; }

        public int GetMaxColumnCharacters(int columnIndex)
        {
            int result = 0;
            foreach(var row in Rows)
            {
                foreach(var value in row.Values)
                {
                    if (value.Length > result)
                        result = value.Length;
                }
            }
            return result;
        }
	}
}
