using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DepFinder.CSharpStratagy.Models
{
	public static class Regexps
	{
		//TODO увиеличить производительность
		public static Regex ContractName = new Regex(@"\s([A-Z]{1,2}C)\s?=\s?(\w{1,})[.]Contracts", RegexOptions.Compiled);
	}
}
