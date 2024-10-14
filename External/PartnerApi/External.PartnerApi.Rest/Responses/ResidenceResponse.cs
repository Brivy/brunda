using System.Text.Json.Serialization;

namespace Brunda.External.PartnerApi.Rest.Responses;

internal record ResidenceResponse
{
    [JsonPropertyName("MakelaarId")]
    public required int RealEstateAgentId { get; init; }
    [JsonPropertyName("MakelaarNaam")]
    public required string RealEstateAgentName { get; init; }
}
