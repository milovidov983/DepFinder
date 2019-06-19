using DepFinder.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DepFinder.Core.Interfaces
{
	public interface ISourceCodeParser
	{
		ExternalDependency[] Parse(string sourceCode);
	}
}
