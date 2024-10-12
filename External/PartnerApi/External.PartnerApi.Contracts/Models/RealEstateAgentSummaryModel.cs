namespace Funda.Assigment.External.PartnerApi.Contracts.Models;

public record RealEstateAgentSummaryModel
{
    public required int RealEstateAgentId { get; init; }
    public required string RealEstateAgentName { get; init; }
    public required int ResidencesForSaleCount { get; init; }
}
