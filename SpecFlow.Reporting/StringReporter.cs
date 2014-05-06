using System.IO;
namespace SpecFlow.Reporting
{
	public abstract class StringReporter : Reporter
	{
		public string WriteToString()
		{
			using (var stream = new MemoryStream())
			{
				WriteToStream(stream);
				stream.Position = 0;
				using (var reader = new StreamReader(stream))
				{
					return reader.ReadToEnd();
				}
			}
		}				
	}
}