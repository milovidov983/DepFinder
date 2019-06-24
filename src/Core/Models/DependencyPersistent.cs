using System;
using System.Collections.Generic;
using System.Text;

namespace DepFinder.Core.Models
{
	public class ProjectDependency
	{
		/// <summary>
		/// Name of the dependent project
		/// </summary>
		public string ProjectName { get; set; }

		/// <summary>
		/// [The name of the external project that implements the contract] -> [Contract model name] -> [Files containing model]
		/// </summary>
		public Dictionary<string, Dictionary<string, HashSet<string>>> Dependencies { get; set; }
	}
}