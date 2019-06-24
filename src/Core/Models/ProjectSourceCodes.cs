using System;
using System.Collections.Generic;
using System.Text;

namespace DepFinder.Core.Models
{
	/// <summary>
	/// Исходные коды проекта
	/// </summary>
	public class ProjectSourceCodes
	{
		public string ProjectName { get; set; }
		public File[] Files { get; set; }

		public class File
		{
			public string Name { get; set; }
			public string SourceCode { get; set; }
		}
	}
}
