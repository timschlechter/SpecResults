using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;

namespace SpecFlow.Reporting
{
    [Binding]
    public static partial class Reporters
    {
        #region Private/Internal

        private static List<Reporter> reporters = new List<Reporter>();

        /// <summary>
        /// Returns the current date/time which is used during the test run. It can set to a fixed
        /// datetime by <see cref="FixedRunTime" />
        /// </summary>
        internal static DateTime CurrentRunTime
        {
            get
            {
                if (FixedRunTime.HasValue)
                {
                    return FixedRunTime.Value;
                }
                return DateTime.Now;
            }
        }

        internal static Step CreateStep(DateTime starttime, MethodBase method, params object[] args)
        {
            var methodName = method.Name;

            var step = new Step
            {
                Title = method.Name,
                Steps = new List<Step>(),
                StartTime = starttime
            };

            var attr = method.GetCustomAttributes(true).OfType<StepDefinitionBaseAttribute>().FirstOrDefault();
            if (attr != null)
            {
                // Handle regex style
                if (!String.IsNullOrEmpty(attr.Regex))
                {
                    step.Title = attr.Regex;

                    for (int i = 0; i < args.Length; i++)
                    {
                        var arg = args[i];
                        var table = arg as TechTalk.SpecFlow.Table;
                        if (table != null)
                        {
                            step.Table = new TableParam
                            {
                                Columns = table.Header.ToList(),
                                Rows = table.Rows.Select(x => x.Keys.ToDictionary(
                                    k => k,
                                    k => x[k]
                                )).ToList()
                            };
                        }
                        else
                        {
                          var titleRegex = new Regex(step.Title);
                          var match = titleRegex.Match(step.Title);
						  if (match.Groups.Count > 1)
						  {
							  step.Title = step.Title.ReplaceFirst(match.Groups[1].Value, args[i].ToString());
						  }
						  else
						  {
							  step.MultiLineParameter = args[i].ToString();
						  }
                        }
                    }
                }
                else
                {
                    if (methodName.Contains('_'))
                    {
                        // underscore style
                        step.Title = methodName.Replace("_", " ");
                        step.Title = step.Title.Substring(step.Title.IndexOf(' ') + 1);

                        var methodInfo = method as MethodInfo;
                        for (int i = 0; i < args.Length; i++)
                        {
                            var arg = args[i];
                            var table = arg as TechTalk.SpecFlow.Table;
                            if (table != null)
                            {
                                step.Table = new TableParam
                                {
                                    Columns = table.Header.ToList(),
                                    Rows = table.Rows.Select(x => x.Keys.ToDictionary(
                                        k => k,
                                        k => x[k]
                                    )).ToList()
                                };
                            }
                            else
                            {
                                var name = methodInfo.GetParamName(i).ToUpper();
                                var value = arg.ToString();
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
                    }
                    else
                    {
                        // pascal naming style
                        throw new NotSupportedException("Pascal naming style not supported yet");
                    }
                }
            }

            return step;
        }

        internal static void ExecuteStep(Action action, params object[] args)
        {
            ExecuteStep(action, null, args);
        }

        internal static void ExecuteStep(Action action, MethodBase methodBase, params object[] args)
        {
            methodBase = methodBase ?? action.Method;

            var currentSteps = new Dictionary<Reporter, Step>();

            var starttime = Reporters.CurrentRunTime;
            foreach (var reporter in Reporters.GetAll())
            {
                currentSteps.Add(reporter, reporter.CurrentStep);

                var step = CreateStep(starttime, methodBase, args);

                var stepContainer = reporter.CurrentStep ?? reporter.CurrentScenarioBlock;
                stepContainer.Steps.Add(step);
                reporter.CurrentStep = step;
                Reporters.OnStartedStep(reporter);
            }

            Exception actionException = null;
            try
            {
                if (action.Method.GetParameters().Count() == 0)
                {
                    action.Method.Invoke(action.Target, null);
                }
                else
                {
                    action.Method.Invoke(action.Target, args);
                }
            }
            catch (Exception ex)
            {
                if (ex is TargetInvocationException && ex.InnerException != null)
                {
                    // Exceptions thrown by ReportingMessageSink are wrapped in a TargetInvocationException
                    actionException = ex.InnerException;
                }
                else
                {
                    actionException = ex;
                }
            }
            finally
            {
                var endtime = Reporters.CurrentRunTime;

                TestResult testResult;
                if (actionException is PendingStepException)
                {
                    testResult = TestResult.Pending;
                }
                else if (actionException != null)
                {
                    testResult = TestResult.Error;
                }
                else
                {
                    testResult = TestResult.OK;
                }

                foreach (var reporter in Reporters.GetAll())
                {
                    reporter.CurrentStep.EndTime = endtime;
                    reporter.CurrentStep.Result = testResult;
                    reporter.CurrentStep.Exception = actionException.ToExceptionInfo();
                    Reporters.OnFinishedStep(reporter);

                    reporter.CurrentStep = currentSteps[reporter];
                }
            }
        }

        #endregion Private/Internal

        #region Public

        /// <summary>
        /// Set fixed start and end times. Usefull for automated tests.
        /// </summary>
        public static DateTime? FixedRunTime
        {
            get;
            set;
        }

        public static Reporter Add(Reporter reporter)
        {
            reporters.Add(reporter);
            return reporter;
        }

        public static IEnumerable<Reporter> GetAll()
        {
            return reporters;
        }

        #endregion Public
    }
}