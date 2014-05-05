using System.IO;

namespace SpecFlow.Reporting
{
	public interface IStreamWriter
	{
		void Write(Stream stream);
	}
}