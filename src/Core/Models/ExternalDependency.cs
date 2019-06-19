using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DepFinder.Core.Models
{
	public class ExternalDependency
	{
		public ExternalDependency()
		{
			Models = new string[] { };
			Project = string.Empty;
		}

		public ExternalDependency(string projectName, string[] modelNames)
		{
			Project = projectName;
			Models = modelNames;
		}

		private string _projectName;
		/// <summary>
		/// Название проекта с внешней зависимостью
		/// </summary>
		public string Project {
			get {
				return _projectName;
			}
			set {
				_projectName = value ?? throw new InvalidOperationException($"Попытка назначать null для поля {nameof(Project)}");
			}
		}

		private string[] _modelNames;
		/// <summary>
		/// Названия классов от которых зависит проект
		/// </summary>
		public string[] Models {
			get {
				return _modelNames;
			}
			set {
				_modelNames = value ?? throw new InvalidOperationException($"Попытка назначать null для поля {nameof(Models)}");
			}
		}

		public override bool Equals(object obj)
		{
			return obj is ExternalDependency right &&
				   Project == right.Project &&
				   _arrayComparer(right.Models);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Project, Models);
		}

		private bool _arrayComparer(string[] right)
		{
			if(_modelNames.Length != right.Length)
			{
				return false;
			}

			var cmpResult = _modelNames.Intersect(right);

			return cmpResult.Count() == _modelNames.Count();

		}
	}
}