using System.Runtime.Remoting.Messaging;

namespace SpecResults.ReportingAspect
{
	internal class ReportingMessageSink : IMessageSink
	{
		public ReportingMessageSink(IMessageSink next)
		{
			NextSink = next;
		}

		public IMessageSink NextSink { get; private set; }

		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			var rtnMsgCtrl = NextSink.AsyncProcessMessage(msg, replySink);
			return rtnMsgCtrl;
		}

		public IMessage SyncProcessMessage(IMessage msg)
		{
			var methodMessage = new MethodCallMessageWrapper((IMethodCallMessage) msg);

			IMethodReturnMessage mrm = null;

			Reporters.ExecuteStep(
				() =>
				{
					var rtnMsg = NextSink.SyncProcessMessage(msg);
					mrm = (IMethodReturnMessage)rtnMsg;

					if (mrm.Exception != null)
					{
						throw mrm.Exception;
					}
				},
				methodMessage.MethodBase,
				methodMessage.Args
				);

			return mrm;
		}
	}
}