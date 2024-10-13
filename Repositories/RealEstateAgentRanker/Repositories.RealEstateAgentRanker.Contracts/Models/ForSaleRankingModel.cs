namespace Funda.Assigment.Repositories.RealEstateAgentRanker.Contracts.Models;

public record ForSaleRankingModel
{
    public required string Name { get; init; }
    public required int ForSaleCount { get; init; }
}