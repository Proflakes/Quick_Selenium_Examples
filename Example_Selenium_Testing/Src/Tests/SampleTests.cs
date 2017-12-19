using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Example_Selenium_Testing.Tests
{
	/// <summary>
	/// Sample tests to be examples of test framework functionality.
	/// </summary>
	[TestFixture()]

	public class SampleTests
	{
		private Browser browser;

		/// <summary>
		/// Initialize browser.
		/// </summary>
		[SetUp]
		public void LoadDriver()
		{
			this.browser = new Browser();
		}

		/// <summary>
		/// Verify two numbers are same. Used as a baseline test to ensure code is working as intended.
		/// </summary>
		[Test, TestCaseSource(typeof(SiteList), "GetTestCases")]
		public void TestNumbers(string site)
		{
			Assert.AreEqual(5, 5);
		}

		/// <summary>
		/// Simple test that will navigate to each test website and verify a successful load by checking that the Title element is not empty.
		/// </summary>
		/// <param name="site"></param>
		[Test, TestCaseSource(typeof(SiteList), "GetTestCases")]
		public void VerifyPageLoad(string site)
		{
			IWebDriver driver = browser.Driver;

			driver.Navigate().GoToUrl(site);

			Assert.IsNotEmpty(driver.Title);
		}

		/// <summary>
		/// Simple test to find out general info about the page such as # of specific elements.
		/// </summary>
		/// <param name="site"></param>
		[Test, TestCaseSource(typeof(SiteList), "GetTestCases")]
		public void InfoGathering(string site)
		{
			IWebDriver driver = browser.Driver;

			driver.Navigate().GoToUrl(site);

			Console.WriteLine("Images: " + driver.FindElements(By.XPath("//img")).Count.ToString());
			Console.WriteLine("Divs: " + driver.FindElements(By.XPath("//div")).Count.ToString());
			Console.WriteLine("Links: " + driver.FindElements(By.XPath("//link")).Count.ToString());
		}

		/// <summary>
		/// Quits the browser.
		/// </summary>
		[TearDown]
		public void UnloadDriver()
		{
			this.browser.Quit();
		}
	}
}

