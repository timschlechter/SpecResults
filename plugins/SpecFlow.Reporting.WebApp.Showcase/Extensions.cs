using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlow.Reporting.WebApp.Showcase
{
	public static class Extensions
	{
		#region IWebDriver

		public static void TakeScreenshot(this IWebDriver driver, string outputFile)
		{
			var outputFolder = Path.GetDirectoryName(outputFile);
			if (!Directory.Exists(outputFolder))
			{
				Directory.CreateDirectory(outputFolder);
			}

			ITakesScreenshot takesScreenshot = driver as ITakesScreenshot;

			if (takesScreenshot != null)
			{
				var screenshot = takesScreenshot.GetScreenshot();

				string screenshotFilePath = Path.Combine(outputFile);

				screenshot.SaveAsFile(screenshotFilePath, ImageFormat.Png);
			}		
		}

		#endregion
	}
}
