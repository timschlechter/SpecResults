using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

namespace SpecResults.ReportingAspect
{
	internal class ReportingProperty : IContextProperty, IContributeObjectSink
	{
		public void Freeze(Context newContext)
		{
		}

		public bool IsNewContextOK(Context newCtx)
		{
			var p = newCtx.GetProperty("Reporting") as ReportingProperty;
			if (p == null)
			{
				return false;
			}
			return true;
		}

		public string Name
		{
			get { return "Reporting"; }
		}

		public IMessageSink GetObjectSink(MarshalByRefObject o, IMessageSink next)
		{
			return new ReportingMessageSink(next);
		}
	}
}