using DepFinder.Core.Interfaces;
using DepFinder.Core.Models;
using DepFinder.Infrastructure;
using DepFinder.Repository;
using DepFinder.Repository.FileStratagy;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DepFinderTests.RepositoryContextTests
{
	public class Tests
	{
		[Fact]
		public void GetAsync_SetOneElement_CallGetSourcesAsync()
		{
			var repos = new Mock<IProjectsRepository>();
			var context = Create.Repository()
				.SetSpecialRepository(repos)
				.SetRepositoryList(RepositoryLists.OneFileSystemEmptyElement)
				.SetGetSourcesAsyncResult(Task.Run(() => new ProjectSourceCodes { }))
				.Build();

			var collection = context.GetAsync();

			repos.Verify(foo => foo.GetSourcesAsync(It.IsAny<IRepositoryData>()), Times.AtLeastOnce());
		}

		[Fact]
		public async Task GetAsync_ThrowException_LoggerCalled()
		{
			var context = Create.Repository()
				.SetGetSourcesAsyncResult(new Exception())
				.SetRepositoryList(RepositoryLists.OneFileSystemEmptyElement)
				.Build();

			await context.GetAsync();

			await Task.Delay(1000);

			Assert.Equal(CodeErrors.HasError, context.error.Status);
		}
	}
}