using System;
using System.Collections.Generic;
using System.Text;

namespace DepFinder.Infrastructure
{
	public class Error
	{
		public CodeErrors Status { get; set; } = CodeErrors.NoError;
		public string Class { get; internal set; }
		public string Method { get; internal set; }
	}
	public enum CodeErrors
	{
		NoError = 1,
		HasError
	}
}
