using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SpecFlow.Reporting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SpecFlow.Reporting.Json
{
	public class JsonReport : Report, ITextWriter
	{
		public void WriteAsText(Stream stream)
		{
			var json = JsonConvert.SerializeObject(
				this,
				new JsonSerializerSettings
				{
					Formatting = Formatting.Indented,
					ContractResolver = new ReportContractResolver(),
					NullValueHandling = NullValueHandling.Ignore,
					Converters = new JsonConverter[] { new StringEnumConverter() }.ToList()
				}
			);
			var bytes = Encoding.Default.GetBytes(json);
			using (var ms = new MemoryStream(bytes))
			{
				ms.CopyTo(stream);
			}
		}
	}
}
