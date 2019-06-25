using DepFinder.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DepFinder.Repository.FileStratagy
{
	public class FileSystemRepository : IRepositoryData
	{
		public string Name { get; set; }
		public string Path { get ; set; }
	}
}
