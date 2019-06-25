using DepFinder.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DepFinder.SourceCodeParser
{
	public class ParserContext : ISourceCodeParser
	{
		private ISourceCodeParser parser;

		public ParserContext(ISourceCodeParser parseStrategy)
		{
			this.parser = parseStrategy;
		}

		public ParserContext SetStratagy(ISourceCodeParser parseStrategy)
		{
			this.parser = parseStrategy ?? throw new ArgumentNullException("parseStrategy is null");
			return this;
		}

		public Dictionary<string, string[]> ExtractDependencies(string sourceCode)
		{
			return parser.ExtractDependencies(sourceCode);
		}

		public string ExtractProjectName(string sourceCode)
		{
			return parser.ExtractProjectName(sourceCode);
		}
	}
}
