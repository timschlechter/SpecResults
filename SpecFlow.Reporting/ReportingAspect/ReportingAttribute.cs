using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using TechTalk.SpecFlow;

namespace SpecFlow.Reporting
{
	
	internal class ReportingAttribute : ContextAttribute
	{
		public ReportingAttribute()
			: base("Reporting")
		{
		}

		public override void GetPropertiesForNewContext(IConstructionCallMessage ccm)
		{
			ccm.ContextProperties.Add(new ReportingProperty());
		}

		public override bool IsContextOK(Context ctx, System.Runtime.Remoting.Activation.IConstructionCallMessage ctorMsg)
		{
			var p = ctx.GetProperty("Reporting") as ReportingProperty;
			if (p == null)
				return false;
			return true;
		}

		public override bool IsNewContextOK(Context newCtx)
		{
			var p = newCtx.GetProperty("Reporting") as ReportingProperty;
			if (p == null)
				return false;
			return true;
		}
	}
}