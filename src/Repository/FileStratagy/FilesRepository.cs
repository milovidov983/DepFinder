using DepFinder.Core.Interfaces;
using DepFinder.Core.Models;
using DepFinder.Repository.FileStratagy;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
				throw new InvalidOperationException($"Path do not exsists! [{path}]");
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

		public async Task<ProjectSourceCodes> GetSourcesAsync(IRepositoryData repository)
		{
			var files = (await GetFiles(repository.Path)).Select(x =>
				   new ProjectSourceCodes.File
				   {
					   Name = x.Name,
					   SourceCode = x.Data
				   }
				).ToArray();

			var project = new ProjectSourceCodes
			{
				Files = files
			};

			project.ProjectName = projectName;

			return project;
		}
		
		private List<(string Name, string Data)> files = new List<(string Name, string Data)>();
		private string projectName = string.Empty;
		private readonly Regex ProjectNamePattern = new Regex(@"(\w{1,})[.]Contracts", RegexOptions.Compiled);
		private const int serviceNameGroupIndex = 1;
		/// <summary>
		/// A side effect function is trying to find a project name using folder names.
		/// </summary>
		private async Task<List<(string Name, string Data)>> GetFiles(string currentFolder)
		{
			
			foreach (string fileName in Directory.EnumerateFiles(currentFolder, "*.cs"))
			{
				var sourceCode = await File.ReadAllTextAsync(fileName);

				files.Add((Name: fileName, Data: sourceCode));
			}
			foreach (string name in Directory.EnumerateDirectories(currentFolder))
			{
				var macth = ProjectNamePattern.Match(name);
				if (macth.Success)
				{
					projectName = macth.Groups[serviceNameGroupIndex].Value;
				}

				return await GetFiles(name);
			}
			return files;
		}
	}
}
