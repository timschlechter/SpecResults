using System;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using TechTalk.SpecFlow;

namespace SpecFlow.Reporting
{
	internal class ReportingAspect : IMessageSink
	{
		private IMessageSink next;

		public ReportingAspect(IMessageSink next)
		{
			this.next = next;
		}

		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			var rtnMsgCtrl = next.AsyncProcessMessage(msg, replySink);
			return rtnMsgCtrl;
		}

		public IMessageSink NextSink
		{
			get { return next; }
		}

		public IMessage SyncProcessMessage(IMessage msg)
		{
			var methodMessage = new MethodCallMessageWrapper((IMethodCallMessage)msg);

			var starttime = Reporters.CurrentRunTime;

			foreach (var reporter in Reporters.reporters)
			{
				var step = Reporters.CreateStep();
				step.StartTime = starttime;
				var attr = methodMessage.MethodBase.GetCustomAttributes(true).OfType<StepDefinitionBaseAttribute>().FirstOrDefault();
				if (attr != null)
				{
					step.Title = attr.Regex;

					var args = (object[])msg.Properties["__Args"];
					for (int i = 0; i < args.Length; i++)
					{
						step.Title = step.Title.ReplaceFirst("(.*)", args[i].ToString());
					}
				}
				reporter.CurrentScenarioBlock.Steps.Add(step);
				reporter.CurrentStep = step;
				Reporters.OnStartedStep(reporter);
			}

			IMessage rtnMsg = next.SyncProcessMessage(msg);
			IMethodReturnMessage mrm = (rtnMsg as IMethodReturnMessage);

			TestResult testResult;
			if (mrm.Exception is PendingStepException)
			{
				testResult = TestResult.Pending;
			}
			else if (ScenarioContext.Current.TestError == null)
			{
				testResult = TestResult.OK;
			}
			else
			{
				testResult = TestResult.Error;
			}

			foreach (var reporter in Reporters.reporters)
			{
				reporter.CurrentStep.Result = testResult;
				Reporters.OnFinishedStep(reporter);
			}

			return mrm;
		}
	}

	internal class ReportingProperty : IContextProperty, IContributeObjectSink
	{
		public IMessageSink GetObjectSink(MarshalByRefObject o, IMessageSink next)
		{
			return new ReportingAspect(next);
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