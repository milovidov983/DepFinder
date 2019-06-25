using DepFinder.Core.Interfaces;
using DepFinder.Core.Models;
using DepFinder.Repository.FileStratagy;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DepFinder.FilesRepository.FileStratagy
{
	public class FilesStrategy : IProjectsRepository
	{
		private string path;

		public FilesStrategy(string path)
		{
			this.path = path;
		}

		public Task<IRepositoryData[]> GetRepositoriesList()
		{
			if(!Directory.Exists(path))
			{
				throw new ArgumentException($"Path do not exsists! [{path}]");
			}

			var dirs = Directory.GetDirectories(path).Select(x =>
			{
				IRepositoryData data = new FileSystemRepository
				{
					Name = x,
					Path = x
				};
				return data;
			}
			).ToArray();

			
			return Task.Run(() => dirs);
		}

		public async Task<ProjectSourceCodes[]> GetSourcesAsync(IRepositoryData repository)
		{
			// Получить список папок


			// Войти в первую папку и рекурсивно считать все файлы с их полными путями

			// положить результат в блокингКоллекшн

			// 4 потока

			// вернуть блокинг колекшн

			throw new NotImplementedException();
		}
	}
}
