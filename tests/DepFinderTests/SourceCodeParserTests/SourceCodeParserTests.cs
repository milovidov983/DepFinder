using ProjectParser;
using System.Linq;
using Xunit;

namespace DepFinderTests.SourceCodeParserTests
{
	public class SourceCodeParserTests
	{
		[Fact]
		public void FindOneDependencyInFileResultOneDependency()
		{
			var sourceFile = TestCases.OneDependencyInMethod;

			var parser = new SourceCodeParser();

			var results = parser.Parse(sourceFile);

			Assert.True(results.Count == 1);
			Assert.Equal("ServiceStation", results.First().Key);
			Assert.Equal("SearchStations", results.First().Value.First());
		}

		[Fact]
		public void FindTwoModelInFunctionSignature()
		{
			var sourceFile = TestCases.TwoModelInFunctionSignature;

			var parser = new SourceCodeParser();

			var results = parser.Parse(sourceFile);

			Assert.True(results.Count == 2);
			Assert.True(results.ContainsKey("C1"));
			Assert.True(results.ContainsKey("C2"));

			var firstModels = results["C1"];
			var secondModels = results["C2"];

			Assert.Equal("Model_A", firstModels.FirstOrDefault(x=> x == "Model_A"));
			Assert.Equal("Model_B", secondModels.FirstOrDefault(x=> x == "Model_B"));
		}

		[Fact]
		public void TwoModelOverCommentsNoResults()
		{
			var sourceFile = TestCases.TwoModelComments;

			var parser = new SourceCodeParser();

			var results = parser.Parse(sourceFile);

			Assert.True(results.Count == 0);
		}
	}
}