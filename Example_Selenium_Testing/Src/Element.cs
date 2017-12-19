using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Example_Selenium_Testing
{
	/// <summary>
	/// Exposes a Selenium web browser element.
	/// </summary>
	/// <example>
	/// To find all the elements with ID "title" in entire page:
	/// <code>
	/// var titles = new Element (By.Id("title"), this.browser.Driver);
	/// </code>
	/// To get the first element in this collection (which is very common pattern):
	/// <code>
	/// var title = titles.First;
	/// </code>
	/// To get all the elements for looping:
	/// <code>
	/// foreach (IWebElement el in titles.All)
	/// {
	/// 	...
	/// }
	/// </code>
	/// </example>
	public class Element
	{
		/// <summary>
		/// All found elements.
		/// </summary>
		private System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> els;

		/// <summary>
		/// A single element. This is usually the first element from list of found candidates.
		/// </summary>
		private IWebElement el;

		/// <summary>
		/// The element that contains the element being searched for.
		/// </summary>
		private ISearchContext ancestor;

		/// <summary>
		/// Initializes a new instance of the <see cref="Example_Selenium_Testing.Element"/> class.
		/// </summary>
		/// <param name="by">A finder selector from <see cref="OpenQA.Selenium.By"/>.</param>
		/// <param name="ancestor">Context this element exists within. Can be browser's driver or another iWebElement item.</param>
		public Element(By by, ISearchContext ancestor)
		{
			this.ancestor = ancestor;
			this.els = this.ancestor.FindElements(by);
		}

		/// <summary>
		/// Returns the number of elements.
		/// </summary>
		/// <returns>Get the number of elements.</returns>
		public int Count()
		{
			return this.els.Count;
		}

		/// <summary>
		/// Gets, or sets, the primary/first element in collection.
		/// </summary>
		/// <value>The first.</value>
		public IWebElement First
		{
			get
			{
				if (this.el == null)
				{
					this.el = this.els.First();
				}
				return this.el;
			}
			set
			{
				this.el = value == null ? this.els.First() : value;
			}
		}

		/// <summary>
		/// Gets, or sets, the last element in collection.
		/// </summary>
		/// <remarks>Useful for finding the final item in a list.</remarks>
		/// <value>The last.</value>
		public IWebElement Last
		{
			get
			{
				if (this.el == null)
				{
					this.el = this.els.Last();
				}
				return this.el;
			}
			set
			{
				this.el = value == null ? this.els.Last() : value;
			}
		}

		/// <summary>
		/// Gets all the elements from collection.
		/// </summary>
		/// <value>All.</value>
		public System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> All
		{
			get
			{
				return this.els;
			}
		}

		/// <summary>
		/// Empties text from elements in collection.
		/// </summary>
		/// <returns>The element.</returns>
		public Element Clear()
		{
			foreach (IWebElement el in this.els)
			{
				el.Clear();
			}
			return this;
		}

		/// <summary>
		/// Enters text into all elements in collection.
		/// </summary>
		/// <param name="text">The text to enter.</param>
		/// <returns>The element.</returns>
		public Element EnterText(string text)
		{
			foreach (IWebElement el in this.els)
			{
				el.SendKeys(text);
			}
			return this;
		}

		/// <summary>
		/// Empties all items in collection, and enters text into them.
		/// </summary>
		/// <returns>The element.</returns>
		public Element ClearAndEnterText(string text)
		{
			foreach (IWebElement el in this.els)
			{
				el.Clear();
				el.SendKeys(text);
			}
			return this;
		}

		/// <summary>
		/// Performs a click on all elements in collection.
		/// </summary>
		/// <returns>The element.</returns>
		public Element LeftClick()
		{
			foreach (IWebElement el in this.els)
			{
				el.Click();
			}
			return this;
		}

		/// <summary>
		/// Performs a click on all elements in collection. If another element is going to be clicked instead,
		/// it will scroll up the by 50% of the current browser window height and try again.
		/// </summary>
		public Element LeftClickOffset(Browser browser)
		{
			foreach (IWebElement el in els)
			{
				try
				{
					el.Click();
				}
				catch (Exception)
				{
					var browserHeight = browser.Driver.Manage().Window.Size.Height / 2;

					IJavaScriptExecutor JSE = (IJavaScriptExecutor)browser.Driver;
					JSE.ExecuteScript("window.scrollBy(0," + -browserHeight + ")", "");
					el.Click();
				}
			}
			return this;
		}

		/// <summary>
		/// Performs a click on first element in collection.
		/// </summary>
		/// <returns>The element.</returns>
		public Element ClickFirst()
		{
			this.els.First().Click();
			return this;
		}

		/// <summary>
		/// Gets the select list element from currently-selected element.
		/// </summary>
		/// <returns>The select element.</returns>
		public SelectElement GetSelectElement()
		{
			return new SelectElement(this.el);
		}

		/// <summary>
		/// Selects the option by value.
		/// </summary>
		/// <param name="value">The option's value.</param>
		/// <returns>The element.</returns>
		public Element SelectOptionByValue(string value)
		{
			foreach (IWebElement el in this.els)
			{
				var selectBox = new SelectElement(el);
				selectBox.SelectByValue(value);
			}
			return this;
		}

		/// <summary>
		/// Selects the option by text.
		/// </summary>
		/// <param name="value">The option's text/innerHTML.</param>
		/// <returns>The element.</returns>
		public Element SelectOptionByText(string value)
		{
			foreach (IWebElement el in this.els)
			{
				var selectBox = new SelectElement(el);
				selectBox.SelectByText(value);
			}
			return this;
		}

		/// <summary>
		/// Selects an option by its index.
		/// </summary>
		/// <remarks>The first option in list is number 0, second is 1, etc.</remarks>
		/// <returns>The element.</returns>
		/// <param name="index">Index.</param>
		public Element SelectOptionByIndex(int index = 1)
		{
			foreach (IWebElement el in this.els)
			{
				var selectBox = new SelectElement(el);
				selectBox.SelectByIndex(index);
			}
			return this;
		}
	}
}

