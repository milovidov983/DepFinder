﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DepFinder.Infrastructure
{
	public interface ILogger
	{
		void Error(string message);
		void Error(Exception e,string message);
	}
}
