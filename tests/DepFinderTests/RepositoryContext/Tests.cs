using DepFinder.Core.Interfaces;
using DepFinder.Infostructure;
using DepFinder.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DepFinderTests.RepositoryContextNamespase
{
	public class Tests
	{


		[Fact]
		public void GetAsync_SetOneElement_CallGetSourcesAsync()
		{
			var repos = new Mock<IProjectsRepository>();
			var list = Task.Run(() => new object[] { It.IsAny<object>() });
			repos.Setup(x => x.GetRepositoriesList<object>()).Returns(list);

			var context = new RepositoryContext(repos.Object);

			var collection = context.GetAsync<object>();

			repos.Verify(foo => foo.GetSourcesAsync(It.IsAny<IRepositoryType<object>>()), Times.AtLeastOnce());
		}

		[Fact]
		public void GetAsync_ThrowException_LoggerCalled()
		{
			var repos = new Mock<IProjectsRepository>();
			repos.Setup(foo => 
				foo.GetSourcesAsync(It.IsAny<IRepositoryType<object>>())).Throws(new Exception()
				);
			var list = Task.Run(() => new IRepositoryType<object>[] { It.IsAny<IRepositoryType<object>>() });
			repos.Setup(x => x.GetRepositoriesList<IRepositoryType<object>>()).Returns(list);

			var sourceCodeParser = Mock.Of<IProjectsRepository>(x =>
				x.GetSourcesAsync(It.IsAny<IRepositoryType<object>>())
			);

			var context = new RepositoryContext(repos.Object);

			var collection = context.GetAsync<object>();

			Assert.Equal(CodeErrors.HasError, context.error.Status);
		}
	}
}
