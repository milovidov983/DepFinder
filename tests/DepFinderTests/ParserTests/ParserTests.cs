using System;
using System.Linq;
using Xunit;

namespace ProjectParser.Tests
{
	public class UnitTest1
	{
		[Fact]
		public void FindOneDependencyInFile()
		{
			var sourceFile = TestCases.OneDependencyInMethod.Source;
			var expected = TestCases.OneDependencyInMethod.ExpectedResults;

			var parser = new Parser();

			var results = parser.Parse(sourceFile);

			Assert.Equal(expected, results.OrderBy(x => x.Project).ToArray());
		}

		[Fact]
		public void FindTwoModelInFunctionSignature()
		{
			var sourceFile = TestCases.TwoModelInFunctionSignature.Source;
			var expected = TestCases.TwoModelInFunctionSignature.ExpectedResults;

			var parser = new Parser();

			var results = parser.Parse(sourceFile);

			Assert.Equal(expected, results.OrderBy(x => x.Project).ToArray());
		}
	}
}