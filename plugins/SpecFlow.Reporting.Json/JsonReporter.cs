using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using System.Linq;
using System.Text;

namespace SpecFlow.Reporting.Json
{
	public class JsonReporter : StringReporter
	{
		public override string Name
		{
			get { return "SpecFlow.Reporting.Json"; }
		}

		public override void WriteToStream(Stream stream)
		{
			var json = JsonConvert.SerializeObject(
				Report,
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