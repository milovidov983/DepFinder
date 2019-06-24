using DepFinder.Core.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DepFinder.Core.Interfaces
{
	public interface IParser
	{
		BlockingCollection<ProjectDependency> Parse(ProjectSourceCodes[] sourceCodes);
	}
}
