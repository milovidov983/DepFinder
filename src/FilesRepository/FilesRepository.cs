using DepFinder.Core.Interfaces;
using DepFinder.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DepFinder.FilesRepository
{
	public class FilesRepository : IProjectsRepository
	{
		public Task<ProjectSourceCodes[]> GetProjectsAsync()
		{
			throw new NotImplementedException();
		}
	}
}
