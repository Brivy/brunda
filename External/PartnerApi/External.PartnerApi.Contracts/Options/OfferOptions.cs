using Funda.Assigment.External.PartnerApi.Contracts.Enums;

namespace Funda.Assigment.External.PartnerApi.Contracts.Options;

public record OfferOptions
{
    public ResidenceContractType ResidenceContractType { get; init; }
    public string? Location { get; init; }
    public bool HasGarden { get; init; }
    public int? Page { get; init; }
    public int? PageSize { get; init; }
}
