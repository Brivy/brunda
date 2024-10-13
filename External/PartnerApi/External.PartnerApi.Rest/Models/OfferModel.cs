using System.Text.Json.Serialization;

namespace Funda.Assigment.External.PartnerApi.Rest.Models;

internal record OfferModel
{
    [JsonPropertyName("Objects")]
    public IReadOnlyCollection<ResidenceModel> Residences { get; init; } = [];
}
