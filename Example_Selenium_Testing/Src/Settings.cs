using System;
using System.Configuration;

namespace Example_Selenium_Testing
{
	/// <summary>
	/// Uses settings from app.config.
	/// </summary>
	public class Settings
	{
		private string name;
		private string defaultValue;

		/// <summary>
		/// Initializes a new instance of the <see cref="Example_Selenium_Testing.Settings"/> class.
		/// </summary>
		/// <param name="name">The setting's key.</param>
		/// <param name="defaultValue">If not found, default value to use.</param>">
		public Settings(string name, string defaultValue = "")
		{
			this.name = name;
			this.defaultValue = defaultValue;
		}

		/// <summary>
		/// Returns the setting as a string.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="Example_Selenium_Testing.Settings"/>. If setting does not exist, empty string is returned.</returns>
		public override string ToString()
		{
			var appSettings = ConfigurationManager.AppSettings;
			return appSettings[this.name] ?? defaultValue;
		}

		/// <summary>
		/// Returns the setting as an integer.
		/// </summary>
		/// <returns>A <see cref="System.Int32"/> that represents the current <see cref="Example_Selenium_Testing.Settings"/>. If setting does not exist, zero is returned.</returns>
		public int ToInt()
		{
			this.defaultValue = this.defaultValue == "" ? "0" : this.defaultValue;

			var appSettings = ConfigurationManager.AppSettings;
			string value = appSettings[this.name] ?? this.defaultValue;
			return Int32.Parse(value);
		}
	}
}

