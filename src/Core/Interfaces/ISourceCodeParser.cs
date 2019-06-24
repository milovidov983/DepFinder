using DepFinder.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DepFinder.Core.Interfaces
{
	public interface ISourceCodeParser
	{
		/// <summary>
		/// Collects dependencies in a file.
		/// </summary>
		/// <param name="sourceCode">Source code</param>
		/// <returns>
		/// Key: project name, 
		/// Value: models
		/// </returns>
		Dictionary<string,string[]> ExtractDependencies(string sourceCode);
		string ExtractProjectName(string sourceCode);
	}
}
