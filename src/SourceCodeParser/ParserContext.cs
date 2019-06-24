using DepFinder.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DepFinder.SourceCodeParser
{
	public class ParserContext
	{
		private ISourceCodeParser parseStrategy;

		public ParserContext(ISourceCodeParser parseStrategy)
		{
			this.parseStrategy = parseStrategy;
		}

		public void SetStratagy(ISourceCodeParser parseStrategy)
		{
			if(parseStrategy == null)
			{
				throw new ArgumentNullException("parseStrategy is null");
			}
			this.parseStrategy = parseStrategy;
		}

		public Dictionary<string, string[]> ExtractDependencies(string sourceCode)
		{
			return parseStrategy.ExtractDependencies(sourceCode);
		}

		public string ExtractProjectName(string sourceCode)
		{
			return parseStrategy.ExtractProjectName(sourceCode);
		}
	}
}
