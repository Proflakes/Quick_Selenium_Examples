using System.Collections.Generic;
using NUnit.Framework;

namespace Example_Selenium_Testing
{
	/// <summary>
	/// Uses data from pre-built list of desired sites to test.
	/// </summary>
	public class SiteList
	{
		/// <summary>
		/// Fetches site list info for test cases.
		/// </summary>
		/// <returns>The test cases.</returns>
		public static IEnumerable<TestCaseData> GetTestCases()
		{
			string[] sites = new string[]
			{
				"https://www.google.com",
				"https://www.yahoo.com",
				"https://www.forbes.com",
				"https://www.wikipedia.org",
				"https://www.github.com"
			};

			foreach (string site in sites)
			{
				yield return new TestCaseData(site);
			}
		}
	}
}