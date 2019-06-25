using DepFinder.Core.Interfaces;
using DepFinder.FilesRepository.FileStratagy;
using DepFinder.Repository.FileStratagy;
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
			var reposPath = Settings.Instanse.PathToTestData + Settings.ReposDirFolder;

			var fileRepos = new FilesStrategy(reposPath);

			var result = await fileRepos.GetRepositoriesList();

			Assert.Equal(3, result.Length);
		}

		[Fact]
		public async Task GetRepositoryList_WrongPath_ThrowArgumentException()
		{

			var fileRepos = new FilesStrategy(string.Empty);

			await Assert.ThrowsAsync<InvalidOperationException>(async () 
				=> await fileRepos.GetRepositoriesList()
				);
		}
		[Fact]
		public async Task GetSourcesAsync_SetOneDependencyInRepository_ReturnOneDependencyFile()
		{
			var reposPath = Settings.Instanse.PathToTestData + Settings.OneDepndencyInRepository;

			var fileRepos = new FilesStrategy(reposPath);
			IRepositoryData data = new FileSystemRepository
			{
				Name = reposPath,
				Path = reposPath
			};
			var result = await fileRepos.GetSourcesAsync(data);

			Assert.Single(result.Files);
		}

		[Fact]
		public async Task GetSourcesAsync_SetOneDependencyInRepository_ReturnProperProjectName()
		{
			var reposPath = Settings.Instanse.PathToTestData + Settings.OneDepndencyInRepository;

			var fileRepos = new FilesStrategy(reposPath);
			IRepositoryData data = new FileSystemRepository
			{
				Name = reposPath,
				Path = reposPath
			};
			var result = await fileRepos.GetSourcesAsync(data);

			Assert.Equal("ServiceName", result.ProjectName);
		}
	}
}
