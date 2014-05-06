using System;
using System.IO;
using System.Text;

namespace SpecFlow.Reporting.Text
{
	public class TextReport : Report, IStreamWriter, IFileWriter
	{
		public void Write(Stream stream)
		{
			var sb = new StringBuilder();
			foreach (var feature in Features)
			{
				sb.AppendLine(feature.ToPlainText());
			}

			var bytes = Encoding.UTF8.GetBytes(sb.ToString());
			using (var ms = new MemoryStream(bytes))
			{
				ms.CopyTo(stream);
			}
		}

		public void WriteFile(string filepath)
		{
			using (var ms = new MemoryStream())
			{
				Write(ms);
				ms.Seek(0, SeekOrigin.Begin);
				using (var fs = File.Create(filepath))
				{
					ms.CopyTo(fs);
				}
			}
		}
	}
}