using System;
using System.Collections.Generic;
using System.Text;

namespace DepFinder.Core
{
	public class DepFinderCore
	{
		public void Start()
		{
			/// получить все исходники
			/// 

			var projectsSources = await projectRepository.GetProjects();

			var projectsSources = new[] {
				new
				{
					Project = "",
					Files = new[]
					{
						new
						{
							Source = "",
							FileName = ""
						}
					}
				}
			};

			/// извлеч из каждого зависимость
			/// 
			var dataset = new[] {
				new {
					ProjectName = "",
					Dependency = new[]
					{
						new
						{
							Projects = new[]
							{
								new
								{
									Name = "P1",
									Files = new [] { "file.1.cs", "file2.cs" },
									Models = new[]
											{
												new
												{
													Name = "M1",
													Files = new [] { "file.1.cs", "file2.cs" }
												}
											}
								}
							}
						}
					}
				}
			};
			/// положить данные в базу
			/// sleep п. 1

		}
	}
}
