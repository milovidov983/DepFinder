namespace DepFinderTests.SourceCodeParserTests
{
	public static class TestCases
	{
		public const string OneDependencyInMethod =@"
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
";

		public const string TwoModelInFunctionSignature = @"
using AAC = C1.Contracts;
using BBC = C2.Contracts;

public void Func(AAC.Model_A m1, BBC.Model_B m2){

}

";

		public const string TwoModelComments = @"
using AAC = C1.Contracts;
using BBC = C2.Contracts;

//public void Func(AAC.Model_A m1){}
///public void Func(BBC.Model_B m2){}

";

	}
}
