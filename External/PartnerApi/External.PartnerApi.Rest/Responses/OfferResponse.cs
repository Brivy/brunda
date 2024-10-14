using System.Text.Json.Serialization;

namespace Brunda.External.PartnerApi.Rest.Responses;

internal record OfferResponse
{
    [JsonPropertyName("Objects")]
    public required IReadOnlyCollection<ResidenceResponse> Residences { get; init; } = [];
    [JsonPropertyName("Paging")]
    public required PageResponse Paging { get; init; }
}
