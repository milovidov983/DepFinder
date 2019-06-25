using DepFinder.Core.Interfaces;
using DepFinder.Repository.FileStratagy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DepFinderTests.RepositoryContextTests
{
	public static class RepositoryLists
	{
		public static Task<IRepositoryData[]> OneFileSystemEmptyElement = Task.Run(() => 
			new IRepositoryData[] {
					new FileSystemRepository {
					}
			});
	}
}
