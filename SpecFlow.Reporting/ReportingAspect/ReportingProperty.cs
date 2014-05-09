using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using TechTalk.SpecFlow;

namespace SpecFlow.Reporting
{
	internal class ReportingProperty : IContextProperty, IContributeObjectSink
	{
		public IMessageSink GetObjectSink(MarshalByRefObject o, IMessageSink next)
		{
			return new ReportingMessageSink(next);
		}

		public void Freeze(Context newContext)
		{
		}

		public bool IsNewContextOK(Context newCtx)
		{
			var p = newCtx.GetProperty("Reporting") as ReportingProperty;
			if (p == null)
				return false;
			return true;
		}

		public string Name
		{
			get { return "Reporting"; }
		}
	}
}