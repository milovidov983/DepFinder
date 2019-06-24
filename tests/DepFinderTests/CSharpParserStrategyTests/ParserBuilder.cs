using DepFinder.CSharpStratagy;
using DepFinder.Infostructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DepFinderTests.SourceCodeParserTests
{
	public class ParserBuilder
	{
		private CSharpParserStrategy _parser;
		public ParserBuilder()
		{
			var logger = Mock.Of<ILogger>();
			_parser = new CSharpParserStrategy(logger);
		}
		public CSharpParserStrategy Build()
		{
			return _parser;
		}
	}
}