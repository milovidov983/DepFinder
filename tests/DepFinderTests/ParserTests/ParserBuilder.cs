using DepFinder.Core;
using DepFinder.Core.Interfaces;
using DepFinder.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DepFinderTests.ParserTests
{
	public class ParserBuilder
	{
		private Parser _parser;
		private ILogger customLogger;
		
		public ParserBuilder SetResult(Dictionary<string, string[]> parsedResult)
		{
			var sourceCodeParser = Mock.Of<ISourceCodeParser>(x =>
				x.ExtractDependencies(It.IsAny<string>()) == parsedResult
			);

			CreateParser(sourceCodeParser);

			return this;
		}

		public ParserBuilder SetResult(Exception e)
		{
			var sourceCodeParser = new Mock<ISourceCodeParser>();
			sourceCodeParser.Setup(foo => foo.ExtractDependencies(It.IsAny<string>())).Throws(e);

			CreateParser(sourceCodeParser.Object);

			return this;
		}

		private void CreateParser(ISourceCodeParser codeParser)
		{
			if (customLogger == null)
			{
				var logger = Mock.Of<ILogger>();
				_parser = new Parser(codeParser, logger);
			}
			else
			{
				_parser = new Parser(codeParser, customLogger);
			}
		}

		public ParserBuilder WithCustomLogger(ILogger logger)
		{
			if(_parser != null)
			{
				_parser.SetLogger(logger);
			}
			customLogger = logger;
			return this;
		}

		public Parser Build()
		{
			if(_parser == null)
			{
				throw new ArgumentNullException($@"Build {nameof(ParserBuilder)} failed!""SetCodeResult"" not called!");
			}
			return _parser;
		}
	}
}
