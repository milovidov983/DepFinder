using DepFinder.FilesRepository.FileStratagy;
using System;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.FileStratagyTests
{
	public class Test
	{
		[Fact]
		public async Task GetRepositoriesList_ReadThreeDir_ReturnThreeDir()
		{
			var globalPath = Settings.Instanse.PathToTestData;

			var fileRepos = new FilesStrategy(globalPath + @"\ReposDirs");

			var result = await fileRepos.GetRepositoriesList();

			Assert.Equal(3, result.Length);
		}
	}
}
