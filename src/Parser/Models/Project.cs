using DepFinder.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DepFinder.Parser.Models
{
	public class Project
	{
		/// <summary>
		/// Имя прокта.
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Список внешних зависимостей проекта.
		/// </summary>
		public ExternalDependency[] ExternalDependencies { get; set; }
	}

	
}
