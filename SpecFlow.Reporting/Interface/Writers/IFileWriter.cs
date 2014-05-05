using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecFlow.Reporting
{
	public interface IFileWriter
	{
		void WriteFile(string filepath);
	}
}
