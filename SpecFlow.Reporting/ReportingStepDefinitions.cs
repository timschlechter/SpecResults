using System;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using TechTalk.SpecFlow;

namespace SpecFlow.Reporting
{
	[Reporter]
	public abstract class ReportingStepDefinitions : ContextBoundObject
	{
	}



	public class ReporterAspect : IMessageSink
	{
		private IMessageSink next;

		public ReporterAspect(IMessageSink next)
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
			var methodMessage = new MethodCallMessageWrapper( (IMethodCallMessage) msg );

			var starttime = Reporter.CurrentRunTime;

			foreach (var state in Reporter.reports)
			{
				var step = state.Factory.CreateStep();
				step.StartTime = starttime;
				var attr = methodMessage.MethodBase.GetCustomAttributes(true).OfType<StepDefinitionBaseAttribute>().FirstOrDefault();
				if (attr != null)
				{
					step.Title = attr.Regex;
				}
				state.CurrentScenarioBlock.Steps.Add(step);
				state.CurrentStep = step;
				Reporter.OnReportingStep(state);
			}

			return next.SyncProcessMessage(msg);
		}
	}

	public class ReporterProperty : IContextProperty, IContributeObjectSink
	{
		public IMessageSink GetObjectSink(MarshalByRefObject o,
		IMessageSink next)
		{
			return new ReporterAspect(next);
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
			get { return "Reporter"; }
		}
	}

	public class ReporterAttribute : ContextAttribute
	{
		public ReporterAttribute()
			: base("Reporter")
		{
		}

		public override void GetPropertiesForNewContext(IConstructionCallMessage ccm)
		{
			ccm.ContextProperties.Add(new ReporterProperty());
		}
	}
}