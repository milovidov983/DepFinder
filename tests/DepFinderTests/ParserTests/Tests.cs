using DepFinder.Core;
using DepFinder.Core.Models;
using DepFinder.Infrastructure;
using Moq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DepFinderTests.ParserTests
{
	public class Tests
	{
		[Fact]
		public void Parse_FindOneModel_ResultOneModel()
		{
			var parser = Create.Parser()
				.SetResult(ParsedResult.OneDependency)
				.Build();


			var results = parser.Parse(SourceCodes.OneSourceFile);
			ProjectDependency[] dependency = GetFrom(results, expectedResultsCount: 1);

			Assert.Single(dependency);

			var dependencies = dependency.First().Dependencies;
			Assert.Single(dependencies);

			var models = dependency.First().Dependencies.First().Value;
			Assert.Single(models);
			Assert.Equal("ModelA", models.First().Key);
		}

		[Fact]
		public void Parse_FindOneProject_ResultOneProject()
		{
			var parser = Create.Parser()
				.SetResult(ParsedResult.OneDependency)
				.Build();


			var results = parser.Parse(SourceCodes.OneSourceFile);
			ProjectDependency[] dependency = GetFrom(results, expectedResultsCount: 1);
			
			Assert.Single(dependency);

			var dependencies = dependency.First().Dependencies;
			Assert.Single(dependencies);

			Assert.Equal("ProjectA", dependency.First().Dependencies.First().Key);
		}


		[Fact]
		public void Parse_FindOneFile_ResultOneFile()
		{
			var parser = Create.Parser()
				.SetResult(ParsedResult.OneDependency)
				.Build();


			var results = parser.Parse(SourceCodes.OneSourceFile);
			ProjectDependency[] dependency = GetFrom(results, expectedResultsCount: 1);

			Assert.Single(dependency);

			var dependencies = dependency.First().Dependencies;
			Assert.Single(dependencies);

			var modelsFiles = dependency.First().Dependencies.First().Value;
			Assert.Single(modelsFiles);

			var files = modelsFiles.First().Value;
			Assert.Single(files);


			Assert.Equal("somefile1.cs", files.First());
		}

		[Fact]
		public void Parse_FindOneProject_ResultHasOneProject()
		{
			var parser = Create.Parser()
				.SetResult(ParsedResult.OneDependency)
				.Build();


			var results = parser.Parse(SourceCodes.SpecialProjectName);
			ProjectDependency[] dependency = GetFrom(results, expectedResultsCount: 1);

			Assert.Single(dependency);
			Assert.Equal("SpecialName", dependency.First().ProjectName);
		}

		[Fact]
		public void Parse_NoFind_ResultEmpty()
		{
			var parser = Create.Parser()
				.SetResult(ParsedResult.Empty)
				.Build();


			var results = parser.Parse(SourceCodes.Empty);
			ProjectDependency[] dependency = GetFrom(results, expectedResultsCount: 1);

			Assert.Empty(dependency);
		}

		[Fact]
		public void Parse_IsNormal_LoggerNoCalled()
		{
			var logger = new Mock<ILogger>();

			var parser = Create.Parser()
				.WithCustomLogger(logger.Object)
				.SetResult(ParsedResult.Empty)
				.Build();


			var results = parser.Parse(SourceCodes.Empty);
			_ = GetFrom(results, expectedResultsCount: 1);

			logger.Verify(foo => foo.Error(It.IsAny<Exception>(), It.IsAny<string>()), Times.Never());
		}


		[Fact]
		public void Parse_ThrowExeption_LoggerCalled()
		{
			var logger = new Mock<ILogger>();

			var parser = Create.Parser()
				.WithCustomLogger(logger.Object)
				.SetResult(new Exception())
				.Build();


			var results = parser.Parse(SourceCodes.NotEmpty);
			_ = GetFrom(results, expectedResultsCount: 1);

			logger.Verify(foo => foo.Error(It.IsAny<Exception>(), It.IsAny<string>()), Times.AtLeastOnce());
		}

		[Fact]
		public void Parse_ThrowExeption_ParseStatusError()
		{
			var parser = Create.Parser()
				.SetResult(new Exception())
				.Build();

			var results = parser.Parse(SourceCodes.NotEmpty);
			_ = GetFrom(results, expectedResultsCount: 1);

			Assert.Equal(CodeErrors.HasError, parser.error.Status);
			Assert.Equal($"{nameof(Parser)}", parser.error.Class);
		}

		private ProjectDependency[] GetFrom(BlockingCollection<ProjectDependency> collection, int expectedResultsCount)
		{
			var results = new List<ProjectDependency>();

			for(var i = 0; i < expectedResultsCount; i++)
			{
				try
				{
					results.Add(collection.Take());
				}
				catch (InvalidOperationException)
				{
					if(results.Count != expectedResultsCount)
					{
						return new ProjectDependency[] { };
					}
				}
			}
			return results.ToArray();
		}
	}
}
