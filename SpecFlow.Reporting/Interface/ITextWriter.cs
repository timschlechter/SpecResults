using System.IO;

namespace SpecFlow.Reporting
{
	public interface ITextWriter
	{
		void WriteAsText(Stream stream);
	}
}