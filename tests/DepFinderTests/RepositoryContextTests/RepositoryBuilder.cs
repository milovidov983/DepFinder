using DepFinder.Core.Interfaces;
using DepFinder.Core.Models;
using DepFinder.Repository;
using Moq;
using System;
using System.Threading.Tasks;

namespace DepFinderTests.RepositoryContextTests
{
	public class RepositoryBuilder
	{
		private Task<IRepositoryData[]> repositoryList;
		private Exception resultException;
		private Task<ProjectSourceCodes> normalResults;
		private Mock<IProjectsRepository> repository;

		public RepositoryBuilder()
		{
			repository = new Mock<IProjectsRepository>();
		}

		public RepositoryBuilder SetRepositoryList(Task<IRepositoryData[]> list)
		{
			repositoryList = list;
			repository.Setup(x => x.GetRepositoriesList()).Returns(() => list);
			return this;
		}

		public RepositoryBuilder SetGetSourcesAsyncResult(Exception exception)
		{
			if(normalResults != null)
			{
				throw new InvalidOperationException("normalResults is not null: result setted yet!");
			}
			resultException = exception;

			return this;
		}
		public RepositoryBuilder SetGetSourcesAsyncResult(Task<ProjectSourceCodes> projectSources)
		{
			if (resultException != null)
			{
				throw new InvalidOperationException("resultException is not null: result setted yet!");
			}
			normalResults = projectSources;

			return this;
		}


		public RepositoryBuilder SetSpecialRepository(Mock<IProjectsRepository> repository)
		{
			this.repository = repository;
			return this;
		}

		public RepositoryContext Build()
		{
			if (resultException == null && normalResults == null)
			{
				throw new ArgumentNullException("No set results! SetGetSourcesAsyncResult() method you must call before build.");
			}
			if(repositoryList == null)
			{
				throw new ArgumentNullException("No set repositoryList! SetRepositoryList() method you must call before build.");
			}
			InitRepository();
			return new RepositoryContext(repository.Object);
		}

		private void InitRepository()
		{
			if (resultException != null)
			{
				repository.Setup(foo =>
					foo.GetSourcesAsync(It.IsAny<IRepositoryData>())).Throws(resultException);
			} else
			{
				repository.Setup(foo =>
					foo.GetSourcesAsync(It.IsAny<IRepositoryData>())).Returns(normalResults);
			}


		}
	}
}
