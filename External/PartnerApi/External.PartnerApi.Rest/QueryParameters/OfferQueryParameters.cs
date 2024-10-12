using Refit;

namespace Funda.Assigment.External.PartnerApi.Rest.QueryParameters;

internal record OfferQueryParameters
{
    [AliasAs("type")]
    public required string ResidenceContractType { get; init; }
    [AliasAs("zo")]
    public string? SearchQuery { get; init; }
    [AliasAs("page")]
    public int? Page { get; init; }
    [AliasAs("pagesize")]
    public int? PageSize { get; init; }
}
