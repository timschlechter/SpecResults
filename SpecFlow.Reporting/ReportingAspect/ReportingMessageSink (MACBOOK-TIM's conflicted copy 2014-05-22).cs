﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
					// Handle regex style
					if (!String.IsNullOrEmpty(attr.Regex))
					{
						step.Title = attr.Regex;

						var args = (object[])msg.Properties["__Args"];
						for (int i = 0; i < args.Length; i++)
						{
							var arg = args[i];
							var table = arg as TechTalk.SpecFlow.Table;
							if (table != null)
							{
								//step.Table = new Table
								//{
								//	Header = table.Header.ToList(),
								//	Rows = table.Rows.Select(x => x.Values.ToList()).ToList()
								//};
							}
							else
							{
								step.Title = step.Title.ReplaceFirst("(.*)", args[i].ToString());
							}
						}
					}
					else
					{
						if (methodMessage.MethodName.Contains('_'))
						{
							// underscore style
							step.Title = methodMessage.MethodName.Replace("_", " ");
							step.Title = step.Title.Substring(step.Title.IndexOf(' ') + 1);

							var methodInfo = methodMessage.MethodBase as MethodInfo;
							var args = methodMessage.Args.ToArray();
							for (int i = 0; i < args.Length; i++)
							{
								var name = methodInfo.GetParamName(i).ToUpper();
								var value = args[i].ToString();
								if (step.Title.Contains(name + " "))
								{
									step.Title = step.Title.ReplaceFirst(name + " ", value + " ");
								}
								else
								{
									step.Title = step.Title.ReplaceFirst(" " + name, " " + value);
								}
							}
						}
						else
						{
							// pascal naming style
							throw new NotSupportedException("Pascal naming style not supported yet");
						}
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