using System;
using SpecResults.ReportingAspect;

namespace SpecResults
{
	[Reporting]
	public abstract class ReportingStepDefinitions : ContextBoundObject
	{
		public void ReportStep(Action action, params object[] args)
		{
			Reporters.ExecuteStep(action, args);
		}
	}
}