using DepFinder.Core;
using DepFinder.Core.Interfaces;
using DepFinder.Core.Models;
using DepFinder.CSharpStratagy;
using DepFinder.FilesRepository.FileStratagy;
using DepFinder.Infrastructure;
using DepFinder.Repository;
using DepFinder.SourceCodeParser;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.ParserTests
{
	public class Tests
	{
		[Fact]
		public async Task Parse_SetOneDependency_ResultOneDependency()
		{
			Parser parser = CreateCSharpParser();

			ProjectSourceCodes sourceSodes = await SourceWithOneDependency();

			var parsedResults = parser.Parse(new ProjectSourceCodes[] { sourceSodes });

			var dependency = parsedResults.Take();

			Assert.Single(dependency?.Dependencies);
		}

		private static Parser CreateCSharpParser()
		{
			var logger = Mock.Of<ILogger>();
			CSharpParserStrategy cSharpParserStrategy = new CSharpParserStrategy(logger);
			ParserContext parserContext = new ParserContext(cSharpParserStrategy);
			var parser = new Parser(parserContext, logger);
			return parser;
		}

		private static async Task<ProjectSourceCodes> SourceWithOneDependency()
		{
			var reposPath = Settings.Instanse.PathToTestData + Settings.OneDepndencyInRepository;
			IProjectsRepository fileRepos = new FilesStrategy(reposPath);
			RepositoryContext repositoryContext = new RepositoryContext(fileRepos);


			var collection = await repositoryContext.GetAsync();

			var sourceSodes = collection.Take();
			return sourceSodes;
		}
	}
}
