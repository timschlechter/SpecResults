using System.Collections.Generic;

namespace SpecFlow.Reporting
{
	public interface ITagged
	{
		List<string> Tags { get; set; }
	}
}