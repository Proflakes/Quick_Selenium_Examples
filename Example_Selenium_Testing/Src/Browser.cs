using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System.Drawing;

namespace Example_Selenium_Testing
{

	/// <summary>
	/// Exposes the Selenium web browser.
	/// </summary>
	public class Browser
	{

		/// <summary>
		/// The Selenium web driver.
		/// </summary>
		private IWebDriver driver;

		/// <summary>
		/// The browser type (either "firefox" or "chrome", as set by app.config file).
		/// </summary>
		public string type;

		/// <summary>
		/// Initializes a new instance of the <see cref="Example_Selenium_Testing.Browser"/> class.
		/// </summary>
		public Browser()
		{
			this.type = new Settings("desiredBrowser").ToString().ToLower();
			switch (this.type)
			{
				case "chrome":
					driver = new ChromeDriver();
					break;

				case "ie":
					driver = new InternetExplorerDriver();
					break;
			}

			if (this.driver != null)
			{
				if (this.driver.ToString().Contains("Chrome"))
				{
					driver.Manage().Window.Size = new Size(1280, 1024);
				}
				else
				{
					driver.Manage().Window.Maximize();
				}
			}
		}

		/// <summary>
		/// Gets the browser title.
		/// </summary>
		/// <remarks>This is document.title in DOM.</remarks>
		/// <value>The title.</value>
		public string Title
		{
			get
			{
				return this.driver.Title;
			}
		}

		/// <summary>
		/// Gets the brower's page source.
		/// </summary>
		/// <value>The page source.</value>
		public string PageSource
		{
			get
			{
				return this.driver.PageSource;
			}
		}

		/// <summary>
		/// Gets the Selenium web driver.
		/// </summary>
		/// <value>The driver.</value>
		public IWebDriver Driver
		{
			get
			{
				return this.driver;
			}
		}

		/// <summary>
		/// Verifies an element exists, or will exist, by the time that timeout expires.
		/// </summary>
		/// <remarks>This is better than sleeping for a time, because this will complete as soon as element is found.</remarks>
		/// <returns><c>true</c>, if element has been found, <c>false</c> otherwise.</returns>
		/// <param name="by">The finder argument for iWebElement.FindElement().</param>
		/// <param name="maxWait">The number of seconds to wait until giving up that element will exist.</param>
		/// <param name="context">The context to search within. If not provided, entire DOM is searched.</param>
		public bool ElementWillExist(By by, int maxWait = 10, ISearchContext context = null)
		{
			context = context ?? this.driver;
			var wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(maxWait));
			bool found = true;
			try
			{
				wait.Until<IWebElement>((driver) =>
					{
						try
						{
							return context.FindElement(by);
						}
						catch
						{
							return null;
						}
					}

				);
			}
			catch
			{
				found = false;
			}
			return found;
		}

		/// <summary>
		/// Verifies an element is visible, or will be visible, by the time that timeout expires.
		/// </summary>
		/// <remarks>This is better than sleeping for a time, because this will complete as soon as element is found.</remarks>
		/// <returns><c>true</c>, if element is visible, <c>false</c> otherwise.</returns>
		/// <param name="by">The finder argument for iWebElement.FindElement().</param>
		/// <param name="maxWait">The number of seconds to wait until giving up that element will exist.</param>
		/// <param name="context">The context to search within. If not provided, entire DOM is searched.</param>
		public bool ElementIsVisible(By by, int maxWait = 10, ISearchContext context = null)
		{
			context = context ?? this.driver;
			var wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(maxWait));
			bool found = true;
			try
			{
				wait.Until<bool>((driver) =>
					{
						try
						{
							IWebElement el = context.FindElement(by);
							return el.Displayed && el.Enabled;
						}
						catch
						{
							return false;
						}
					}

				);
			}
			catch
			{
				found = false;
			}
			return found;
		}

		/// <summary>
		/// Navigate to a URL.
		/// </summary>
		/// <returns><c>true</c>, if browser successfully navigated to URL, <c>false</c> otherwise.</returns>
		/// <param name="URL">Full URL</param>
		/// <param name="by">The finder argument for iWebElement.FindElement().</param>
		/// <param name="maxWait">The number of seconds to wait until giving up that element will exist.</param>
		public bool GoToUrl(String URL, By by = null, int maxWait = 3)
		{
			driver.Navigate().GoToUrl(URL);
			// Throw an exception if we have to wait too long.
			if (by != null)
			{
				if (!this.ElementWillExist(by, maxWait))
				{
					throw new Exception(String.Format(
						"Tried to navigate to {0}, but failed because could not find requested element.",
						URL
					));
				}
			}

			return true;
		}

		/// <summary>
		/// Focuses on certain iframe.
		/// </summary>
		/// <remarks>
		/// Focus on an iframe.
		/// </remarks>
		/// <param name="by">The selector by which to find the iframe.</param>
		public void FocusFrame(By by)
		{
			var frame = new Element(by, this.Driver).First;
			this.Driver.SwitchTo().Frame(frame);
		}

		/// <summary>
		/// Find the specified element.
		/// </summary>
		/// <param name="Identifier">Identifier.</param>
		public System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> Find(By Identifier)
		{
			return driver.FindElements(Identifier);
		}

		/// <summary>
		/// Resizes the browser to the dimensions provided.
		/// </summary>
		/// <param name="width">Set the width of the browser.</param>
		/// <param name="height">Set the height of the browser.</param>
		public void ResizeBrowser(int width, int height)
		{
			driver.Manage().Window.Size = new Size(width, height);
		}

		/// <summary>
		/// Deconstruct this the browser.
		/// </summary>
		public void Quit()
		{
			driver.Quit();
		}
	}
}

