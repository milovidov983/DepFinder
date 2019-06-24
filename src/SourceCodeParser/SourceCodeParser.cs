using DepFinder.Core.Interfaces;
using DepFinder.Core.Models;
using DepFinder.Parser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProjectParser
{
	public class SourceCodeParser : ISourceCodeParser
	{
		public Dictionary<string, string[]> Parse(string sourceCode)
		{
			var lines = SplitAndClear(sourceCode);
			var contracts = ExtractContracts(lines);

			if (contracts.Count > 0)
			{
				var result = lines
					.Select( line => GetModels(line, contracts))
					.Where( models => models.Length > 0)
					.SelectMany( models => models)
					.GroupBy( model => model.ProjectName)
					.ToDictionary(
						x=> x.Key,
						x => x.Select(y => y.Name).ToHashSet().ToArray()
					);

				return result;
			}

			return new Dictionary<string, string[]>();
		}

		private Dictionary<string, string> ExtractContracts(IEnumerable<string> lines)
		{
			return lines
				.Where(x => x.Trim().StartsWith("using "))
				.Where(x => x.Contains("="))
				.Select(x => GetContractName(x))
				.Where(x => x != null)
				.ToDictionary(
					x => x.Abbreviation,
					x => x.ProjectName
				);
		}

		private static IEnumerable<string> SplitAndClear(string file)
		{
			return file
				.Split(";")
				.SelectMany(line => line.Split(Environment.NewLine))
				.Where(line => !string.IsNullOrEmpty(line))
				.Where(line => !line.StartsWith(@"///"))
				.Where(line => !line.StartsWith(@"//"));
		}

		private Model[] GetModels(string line, Dictionary<string, string> contracts)
		{
			var models = new HashSet<Model>();
			foreach (var contract in contracts)
			{
				var pattern = @"\s?(" + contract.Key + @").(\w{1,})";
				var match = Regex.Match(line, pattern);

				if (match.Success)
				{
					models.Add(
						new Model
						{
							Name = match.Groups[2].Value,
							ProjectName = contract.Value
						}
					);
				}

			}

			return models.ToArray();
		}

		private Contract GetContractName(string line)
		{
			var match = Eject.ContractName(line);

			if (match.Success)
			{
				var serviceName = match.Groups[2].Value;
				var abbreviation = match.Groups[1].Value;

				return new Contract(serviceName, abbreviation);
			}

			return null;
		}

		private class Contract
		{
			public Contract(string serviceName, string abbreviation)
			{
				ProjectName = serviceName;
				Abbreviation = abbreviation;
			}

			public string ProjectName { get; set; }
			public string Abbreviation { get; set; }
		}

	}
}
