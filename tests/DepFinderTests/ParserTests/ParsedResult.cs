using System;
using System.Collections.Generic;
using System.Text;

namespace DepFinderTests.ParserTests
{
	public static class ParsedResult
	{
		public static Dictionary<string, string[]> OneDependency = new Dictionary<string, string[]>()
		{
			{
				"ProjectA", new string[]{ "ModelA" }
			}
		};
	}
}
