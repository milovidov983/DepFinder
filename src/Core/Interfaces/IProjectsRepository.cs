using DepFinder.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DepFinder.Core.Interfaces
{
	public interface IProjectsRepository
	{
		Task<ProjectSourceCodes[]> GetSourcesAsync(IRepositoryData repository);
		Task<IRepositoryData[]> GetRepositoriesList();
	}
}
