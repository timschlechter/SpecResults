using System.IO;

namespace SpecFlow.Reporting
{
	public interface IBinaryWriter
	{
		void WriteAsBinary(Stream stream);
	}
}