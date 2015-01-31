using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using TechTalk.SpecFlow;

namespace SpecResults
{
    internal class ReportingMessageSink : IMessageSink
    {
        private IMessageSink next;

        public ReportingMessageSink(IMessageSink next)
        {
            this.next = next;
        }

        public IMessageSink NextSink
        {
            get { return next; }
        }

        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            var rtnMsgCtrl = next.AsyncProcessMessage(msg, replySink);
            return rtnMsgCtrl;
        }

        public IMessage SyncProcessMessage(IMessage msg)
        {
            var methodMessage = new MethodCallMessageWrapper((IMethodCallMessage)msg);

            IMethodReturnMessage mrm = null;

            Reporters.ExecuteStep(
                () =>
                {
                    IMessage rtnMsg = next.SyncProcessMessage(msg);
                    mrm = (rtnMsg as IMethodReturnMessage);

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