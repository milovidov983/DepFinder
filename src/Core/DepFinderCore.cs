using DepFinder.Core.Interfaces;
using DepFinder.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepFinder.Core
{
	public class DepFinderCore
	{
		private IProjectsRepository projectRepository { get; set; }
		
		private IParser parser { get; set; }

		public async Task Start()
		{
			/// получить все исходники
			/// 

			var projectsSources = await projectRepository.GetSourcesAsync();

			/// извлеч из каждого зависимость
			/// 


			/// положить данные в базу
			/// sleep п. 1

		}
	}
}
