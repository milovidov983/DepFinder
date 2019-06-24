using DepFinder.Core.Interfaces;
using DepFinder.Core.Models;
using DepFinder.Infostructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DepFinder.FilesRepository
{
	public class FilesStrategyy : IProjectsRepository
	{
		private ILogger logger { get; set; }
		private SemaphoreSlim semaphore = new SemaphoreSlim(8, 8);
		public void SetLogger(ILogger logger) { this.logger = logger; }
		public readonly Error error = new Error();

		public FilesStrategyy(ILogger logger, int threadCount = 8)
		{
			this.logger = logger;
			this.semaphore = new SemaphoreSlim(threadCount, threadCount);
		}

		public Task<ProjectSourceCodes[]> GetSourcesAsync()
		{
			// Получить список папок

			// Войти в первую папку и рекурсивно считать все файлы с их полными путями

			// положить результат в блокингКоллекшн

			// 4 потока

			// вернуть блокинг колекшн

			throw new NotImplementedException();
		}

		public BlockingCollection<ProjectSourceCodes> GetAsync()
		{
			var dependencyCollection = new BlockingCollection<ProjectSourceCodes>();

			var repositoryList = new string[]{ };

			_ = Task.Run(async () =>
			{
				try
				{
					foreach (var folder in repositoryList)
					{
						semaphore.Wait();
						await Task.Run(() =>
						{
							object files = null;// Get(folder);
							dependencyCollection.Add((ProjectSourceCodes)files);
						});
						semaphore.Release();
					}
				}
				finally
				{
					dependencyCollection.CompleteAdding();
				}
			});

			return dependencyCollection;
		}
	}
}
