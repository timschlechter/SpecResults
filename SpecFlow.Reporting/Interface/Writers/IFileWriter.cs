namespace SpecFlow.Reporting
{
	public interface IFileWriter
	{
		string DefaultFileName { get; }

		void WriteFile(string filepath);
	}
}