using DepFinder.Core.Interfaces;
using DepFinder.Core.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepFinder.Core
{
	public class Parser : IParser
	{
		private ISourceCodeParser codeParser { get; set; }
		public Parser(ISourceCodeParser codeParser)
		{
			this.codeParser = codeParser;
		}

		public BlockingCollection<ProjectDependency> Parse(ProjectSourceCodes[] sourceCodes)
		{

			var dependencyCollection = new BlockingCollection<ProjectDependency>();

			_ = Task.Run(() =>
			{
				try
				{
					foreach (var sourceCode in sourceCodes)
					{
						var dependencies = Parse(sourceCode);
						if (dependencies?.Dependencies?.Count > 0)
						{
							dependencyCollection.Add(Parse(sourceCode));
						}
					}
				} finally
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

			var dependencies = projectDependency.Dependencies = new Dictionary<string, Dictionary<string, HashSet<string>>>();
			var allFiles = sourceCode?.Files ?? new ProjectSourceCodes.File[] { };
			foreach (var file in allFiles)
			{
				var projectsModels = codeParser.Parse(file.SourceCode);

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
			return projectDependency;
		}
	}
}
