using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DepFinder.Parser.Models
{
	public class Eject
	{
		public static Match ContractName(string source) => Regexps.ContractName.Match(source);
	}
}
