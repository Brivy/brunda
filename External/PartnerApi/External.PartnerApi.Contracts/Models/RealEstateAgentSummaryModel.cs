namespace Funda.Assigment.External.PartnerApi.Contracts.Models;

public record RealEstateAgentSummaryModel
{
    public required string RealEstateAgentName { get; init; }
    public required int ForSaleCount { get; init; }
}
