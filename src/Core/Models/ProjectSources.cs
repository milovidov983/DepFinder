using System;
using System.Collections.Generic;
using System.Text;

namespace DepFinder.Core.Models
{
	/// <summary>
	/// Исходные коды проекта
	/// </summary>
	public class ProjectSources
	{
		public string ProjectName { get; set; }
		public File[] Files { get; set; }

		public class File
		{
			public string FileName { get; set; }
			public string SourceCode { get; set; }
		}
	}
}
