using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using TechTalk.SpecFlow;

namespace SpecFlow.Reporting
{
	internal class ReportingMessageSink : IMessageSink
	{
		private IMessageSink next;

		public ReportingMessageSink(IMessageSink next)
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
				var step = new Step
				{
					Steps = new List<Step>(),
					StartTime = starttime
				};

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

			var endtime = DateTime.Now;

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
				reporter.CurrentStep.EndTime = endtime;
				reporter.CurrentStep.Result = testResult;
				Reporters.OnFinishedStep(reporter);
			}

			return mrm;
		}
	}
}