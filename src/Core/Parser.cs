using DepFinder.Core.Interfaces;
using DepFinder.Core.Models;
using DepFinder.Infostructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DepFinder.Core
{
	public class Parser : IParser
	{
		private ISourceCodeParser codeParser { get; set; }
		private ILogger logger { get; set; }
		private SemaphoreSlim semaphore = new SemaphoreSlim(8, 8);
		public void SetLogger(ILogger logger) { this.logger = logger; }
		public readonly Error error = new Error();
		public Parser(ISourceCodeParser codeParser, ILogger logger, int threadCount = 8)
		{
			this.codeParser = codeParser;
			this.logger = logger;
			this.semaphore = new SemaphoreSlim(threadCount, threadCount);
		}

		public BlockingCollection<ProjectDependency> Parse(ProjectSourceCodes[] sourceCodes)
		{
			var dependencyCollection = new BlockingCollection<ProjectDependency>();

			_ = Task.Run(async () =>
			{
				try
				{
					foreach (var sourceCode in sourceCodes)
					{
						semaphore.Wait();
						await Task.Run(() =>
						{
							var dependencies = Parse(sourceCode);
							dependencyCollection.Add(dependencies);
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
		private ProjectDependency Parse(ProjectSourceCodes sourceCode)
		{
			var projectDependency = new ProjectDependency
			{
				ProjectName = sourceCode.ProjectName
			};
			ProjectSourceCodes.File currentFile = null;
			try
			{
				var dependencies = projectDependency.Dependencies = new Dictionary<string, Dictionary<string, HashSet<string>>>();
				var allFiles = sourceCode?.Files ?? new ProjectSourceCodes.File[] { };
				foreach (var file in allFiles)
				{
					currentFile = file;
					var projectsModels = codeParser.ExtractDependencies(file.SourceCode);

					foreach (var projectModels in projectsModels)
					{
						var projectName = projectModels.Key;
						var models = projectModels.Value;

						if (dependencies.TryAdd(projectName, null))
						{
							var files = new HashSet<string>
						{
							file.Name
						};

							dependencies[projectName] = models.ToDictionary(
								x => x,
								x => files
							);
						}
						else
						{
							var currentProject = dependencies[projectName];
							foreach (var model in models)
							{
								if (currentProject.ContainsKey(model))
								{
									currentProject[model].Add(file.Name);
								}
								else
								{
									var files = new HashSet<string>
								{
									file.Name
								};
									currentProject.Add(model, files);
								}
							}

						}
					}
				}
			} catch(Exception e)
			{
				error.Status = CodeErrors.HasError;
				error.Class = $"{nameof(Parser)}";
				error.Method = $"{nameof(Parse)}({nameof(ProjectSourceCodes)} sourceCode)";

				logger.Error(e, $"Parsing error in file [{currentFile?.Name}]!");
			}
			return projectDependency;

		}
	}
}
