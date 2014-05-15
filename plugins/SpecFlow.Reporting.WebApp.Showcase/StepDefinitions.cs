using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.IO;
using System.Linq;
using TechTalk.SpecFlow;

namespace SpecFlow.Reporting.WebApp.Showcase
{
	[Binding]
	public class StepDefinitions : ReportingStepDefinitions
	{
		private static IWebDriver WebDriver { get; set; }

		[BeforeTestRun]
		public static void BeforeTestRun()
		{
			var webApp = new WebAppReporter();
			webApp.Settings.Title = "WebAppReporter Showcase";
			webApp.Settings.StepDetailsTemplateFile = @"templates\step-details.tpl.html";
			webApp.Settings.CssFile = @"templates\styles.css";
			webApp.Settings.DashboardTextFile = @"templates\dashboard-text.md";

			Reporters.Add(webApp);

			if (Directory.Exists("screenshots"))
			{
				Directory.Delete("screenshots", true);
			}

			Reporters.FinishedStep += (sender, args) =>
			{
				var path = Path.Combine("screenshots", Guid.NewGuid().ToString() + ".png");
				WebDriver.TakeScreenshot(path);
				args.Step.UserData = new
				{
					Screenshot = path
				};
			};

			Reporters.FinishedReport += (sender, args) =>
			{
				var reporter = args.Reporter as WebAppReporter;
				if (reporter != null)
				{
					reporter.WriteToFolder("app", true);

					Directory.Move("screenshots", @"app\screenshots");
				}
			};

			WebDriver = new FirefoxDriver();
		}

		[AfterTestRun]
		public static void AfterTestRun()
		{
			WebDriver.Close();
			WebDriver.Dispose();
			WebDriver = null;
		}

		[Given(@"I'm on ""(.*)""")]
		public void GivenIMOn(string url)
		{
			WebDriver.Navigate().GoToUrl(url);
		}

		[When(@"I enter searchtext ""(.*)"" in ""(.*)""")]
		public void WhenIEnterSearchtextIn(string searchText, string searchInputId)
		{
			WebDriver.FindElement(By.Id(searchInputId)).SendKeys(searchText);
		}

		[When(@"I click the search button ""(.*)""")]
		public void WhenIClickTheSearchButton(string buttonId)
		{
			WebDriver.FindElement(By.Id(buttonId)).Click();
		}

		[When(@"I click the result with title ""(.*)""")]
		public void WhenIClickTheResultWithTitle(string title)
		{
			WebDriver.FindElements(By.TagName("a"))
				.Where(a => a.Text == title)
				.First()
				.Click();
		}

		[Then(@"I can read the instructions on how to install the package")]
		public void ThenICanReadTheInstructionsOnHowToInstallThePackage()
		{
		}
	}
}