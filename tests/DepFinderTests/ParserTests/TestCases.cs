using DepFinder.Core.Models;
using System.Linq;

namespace ProjectParser.Tests
{
	public class TestCases
	{
		public static Case OneDependencyInMethod =
			new Case
			{
				Source = @"
using SSC = ServiceStation.Contracts;

namespace RequestSearch.Service {
	public static class Ext {
		public static async Task<int[]> GetServiceStationIdResponsibleAsync(this IRabbitHub hub, string responsibleUserId) {
			if (responsibleUserId != null) {
				var response = await hub.GetContentAsync<SSC.SearchStations.Response, SSC.SearchStations.Request>(
				SSC.SearchStations.Topic,
				new SSC.SearchStations.Request {
					UserId = responsibleUserId,
					Limit = 100,
					Offset = 0
				});

				return response.Items.Select(x => x.Id).ToArray();
			}
			return Array.Empty<int>();
		}
	}
}
",
				ExpectedResults = (new ExternalDependency[] {
					new ExternalDependency
					{
						Project = "ServiceStation",
						Models = new[] { "SearchStations" }
					}
				}).OrderBy(x => x.Project).ToArray()
			};



		public static Case TwoModelInFunctionSignature =
				new Case
				{
					Source = @"
using SSC = ServiceStation.Contracts;
using SCC = ServiceStation2.Contracts;

public void Func(SSC.Model m1, SCC.Model m2){

}

",
					ExpectedResults = (new ExternalDependency[] {
							new ExternalDependency
							{
								Project = "ServiceStation",
								Models = new[] { "Model" }
							},
							new ExternalDependency
							{
								Project = "ServiceStation2",
								Models = new[] { "Model" }
							}
						}).OrderBy(x => x.Project).ToArray()
				};

	}

	public class Case
	{
		public string Source { get; set; }
		public ExternalDependency[] ExpectedResults { get; set; }
	}
}
