using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

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