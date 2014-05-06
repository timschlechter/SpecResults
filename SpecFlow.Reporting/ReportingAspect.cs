using System;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using TechTalk.SpecFlow;

namespace SpecFlow.Reporting
{
	public class ReportingAspect : IMessageSink
	{
		private IMessageSink next;

		public ReportingAspect(IMessageSink next)
		{
			this.next = next;
		}

		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotImplementedException();
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
				Reporters.OnStartingStep(reporter);
			}

			return next.SyncProcessMessage(msg);
		}
	}

	public class ReportingProperty : IContextProperty, IContributeObjectSink
	{
		public IMessageSink GetObjectSink(MarshalByRefObject o,
		IMessageSink next)
		{
			return new ReportingAspect(next);
		}

		public void Freeze(Context newContext)
		{
		}

		public bool IsNewContextOK(Context newCtx)
		{
			return true;
		}

		public string Name
		{
			get { return "Reporting"; }
		}
	}

	public class ReportingAttribute : ContextAttribute
	{
		public ReportingAttribute()
			: base("Reporting")
		{
		}

		public override void GetPropertiesForNewContext(IConstructionCallMessage ccm)
		{
			ccm.ContextProperties.Add(new ReportingProperty());
		}
	}
}