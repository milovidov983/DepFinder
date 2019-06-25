using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationTests
{
	public class Settings
	{
		/// <summary>
		/// Path to data files for integration tests
		/// </summary>
		public string PathToTestData { get; set; } = @"D:\Source\DepFinder\tests\Data\";
		public static Settings Instanse { get; set; } = new Settings();

		public static string ReposDirFolder { get; set; } = "ReposDirs";
		public static string OneDepndencyInRepository { get; set; } = @"Repository\one-dependency";

		Settings()
		{
			// TODO
			// PathToTestData брать из env
		}
	}
}