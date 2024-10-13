using System.Text.Json.Serialization;

namespace Funda.Assigment.External.PartnerApi.Rest.Models;

internal record ResidenceModel
{
    [JsonPropertyName("MakelaarId")]
    public required int RealEstateAgentId { get; init; }
    [JsonPropertyName("MakelaarNaam")]
    public required string RealEstateAgentName { get; init; }
}
