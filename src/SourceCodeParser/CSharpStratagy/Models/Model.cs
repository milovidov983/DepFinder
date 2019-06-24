using System;
using System.Collections.Generic;
using System.Text;

namespace DepFinder.CSharpStratagy.Models
{
	public class Model
	{
		public string ProjectName { get; set; }	
		public string Name { get; set; }

		public override bool Equals(object obj)
		{
			return obj is Model model &&
				   ProjectName == model.ProjectName &&
				   Name == model.Name;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(ProjectName, Name);
		}
	}
}
