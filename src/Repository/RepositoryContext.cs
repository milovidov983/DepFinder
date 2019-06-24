using DepFinder.Core.Interfaces;
using DepFinder.Core.Models;
using DepFinder.Infostructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DepFinder.Repository
{
	public class RepositoryContext
	{
		private IProjectsRepository repository;
		private SemaphoreSlim semaphore = new SemaphoreSlim(8, 8);
		private ILogger logger { get; set; }
		public void SetLogger(ILogger logger) { this.logger = logger; }
		public readonly Error error = new Error();


		public RepositoryContext(IProjectsRepository repository, int threadCount = 8)
		{
			this.repository = repository;
			this.semaphore = new SemaphoreSlim(threadCount, threadCount);
		}

		public RepositoryContext SetStratagy(IProjectsRepository repository)
		{
			this.repository = repository ?? throw new ArgumentNullException("Stratagy must not be null!");
			return this;
		}

		public async Task<BlockingCollection<ProjectSourceCodes[]>> GetAsync<RepositoryType>()
		{
			var sources = new BlockingCollection<ProjectSourceCodes[]>();

			var repositoryList = await repository.GetRepositoriesList<IRepositoryType<RepositoryType>>();
			_ = Task.Run(async () =>
			{
				try
				{
					foreach (var repository in repositoryList)
					{
						semaphore.Wait();
						await Task.Run(async () =>
						{
							var files = await this.repository.GetSourcesAsync(repository);
							sources.Add(files);
						});
						semaphore.Release();
					}
				}
				finally
				{
					sources.CompleteAdding();
				}
			});

			return sources;
		}

		private async Task<ProjectSourceCodes[]> GetSourceCodes<RepositoryType>(IRepositoryType<RepositoryType> repository)
		{
			try
			{
				return await this.repository.GetSourcesAsync(repository);
			} catch(Exception e)
			{
				error.Status = CodeErrors.HasError;
				error.Class = $"{nameof(RepositoryContext)}";
				error.Method = $"{nameof(GetSourceCodes)}({nameof(RepositoryType)} repository)";

				logger.Error(e, $"GetSourceCodes error in repository [{repository?.Name}]!");
			}
			return new ProjectSourceCodes[] { };
		}
	}
}
