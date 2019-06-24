using DepFinder.Core.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DepFinderTests.ParserTests
{
	public class ParserTests
	{
		//[Fact]
		public void Test1()
		{
			var parser = Create.Parser().With(ParsedResult.OneDependency).Build();


			var results = parser.Parse(SourceCodes.Full);

			ProjectDependency dependency = null;
			try
			{
				dependency = results.Take();
			}
			catch (InvalidOperationException)
			{
				Assert.Equal("Предполагаем не пустая коллекция", "Фактически пустая коллекция!");
			}

			Assert.Equal("TestProject", dependency.ProjectName);

			Assert.True(dependency.Dependencies.Count == 1);
			var externalProjectName = "ProjectA";
			var externalProject = dependency.Dependencies.First();
			Assert.Equal(externalProjectName, externalProject.Key);


			Assert.True(externalProject.Value.Count == 1);
			var externalModelName = "ModelA";
			var externalModel = externalProject.Value.First();
			Assert.Equal(externalModelName, externalModel.Key);


			Assert.True(externalModel.Value.Count == 1);
			var internalFileName = "somefile1.cs";
			var file = externalModel.Value.First();
			Assert.Equal(internalFileName, file);
		}

		[Fact]
		public void ParseDataWithOneDependencyProject_ResultHasOneDependencyProject()
		{
			var parser = Create.Parser()
				.With(ParsedResult.OneDependency)
				.Build();


			var results = parser.Parse(SourceCodes.OneSourceFile);
			ProjectDependency[] dependency = GetFrom(results, 1);
			
			Assert.Single(dependency);

			var dependencies = dependency.First().Dependencies;
			Assert.Single(dependencies);

			Assert.Equal("ProjectA", dependency.First().Dependencies.First().Key);
		}


		[Fact]
		public void ParseDataWithOneDependencyFile_ResultHasOneDependencyFile()
		{
			var parser = Create.Parser()
				.With(ParsedResult.OneDependency)
				.Build();


			var results = parser.Parse(SourceCodes.OneSourceFile);
			ProjectDependency[] dependency = GetFrom(results, 1);

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
		public void ParseDataWithOneSourceFile_ResultHasOneSourceFile()
		{
			var parser = Create.Parser()
				.With(ParsedResult.OneDependency)
				.Build();


			var results = parser.Parse(SourceCodes.SpecialProjectName);
			ProjectDependency[] dependency = GetFrom(results, 1);

			Assert.Single(dependency);
			Assert.Equal("SpecialName", dependency.First().ProjectName);
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
