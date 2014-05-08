using System.IO;
namespace SpecFlow.Reporting
{
	public abstract class Reporter
	{
		public string Name
		{
			get
			{
				return this.GetType().FullName;
			}
		}

		public Report Report { get; internal set; }

		public Feature CurrentFeature { get; internal set; }

		public Scenario CurrentScenario { get; internal set; }

		public ScenarioBlock CurrentScenarioBlock { get; internal set; }

		public Step CurrentStep { get; internal set; }

		public abstract void WriteToStream(Stream stream);

		public virtual void WriteToFile(string filepath)
		{
			using (var ms = new MemoryStream())
			{
				WriteToStream(ms);
				ms.Seek(0, SeekOrigin.Begin);
				using (var fs = File.Create(filepath))
				{
					ms.CopyTo(fs);
				}
			}
		}
	}
}