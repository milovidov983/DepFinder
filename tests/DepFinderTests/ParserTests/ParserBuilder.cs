using DepFinder.Core;
using DepFinder.Core.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DepFinderTests.ParserTests
{
	public class ParserBuilder
	{
		private Parser _parser;
		
		public ParserBuilder With(Dictionary<string, string[]> parsedResult)
		{
			var sourceCodeParser = Mock.Of<ISourceCodeParser>(x =>
				x.Parse(It.IsAny<string>()) == parsedResult
			);
			_parser = new Parser(sourceCodeParser);
			return this;
		}
		public Parser Build()
		{
			if(_parser == null)
			{
				throw new ArgumentNullException(@"""With"" not called!");
			}
			return _parser;
		}
	}
}
