using System;
using System.Collections.Generic;
using System.Text;

namespace DepFinder.Core.Interfaces
{
	public interface IRepositoryType<TRepository>
	{
		string Name { get; set; }
		TRepository Data { get; set; }
	}
}
