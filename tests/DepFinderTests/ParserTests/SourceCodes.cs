using DepFinder.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DepFinderTests.ParserTests
{
	public static class SourceCodes
	{
		public static ProjectSourceCodes[] OneSourceFile = new ProjectSourceCodes[]
			{
				new ProjectSourceCodes
				{
					Files = new ProjectSourceCodes.File[]
					{
						new ProjectSourceCodes.File
						{
							Name = "somefile1.cs"
						}
					}
				}
			};
		public static ProjectSourceCodes[] SpecialProjectName = new ProjectSourceCodes[]
		{
					new ProjectSourceCodes
					{
						ProjectName = "SpecialName",
						Files = new ProjectSourceCodes.File[]
						{
							new ProjectSourceCodes.File
							{
								Name = ""
							}
						}
					}
		};
		public static ProjectSourceCodes[] Full = new ProjectSourceCodes[]
		{
					new ProjectSourceCodes
					{
						ProjectName = "SpecialName",
						Files = new ProjectSourceCodes.File[]
						{
							new ProjectSourceCodes.File
							{
								Name = "somefile1.cs",
								SourceCode = "... source code ..."
							}
						}
					}
		};
	}
}
