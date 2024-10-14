namespace Brunda.Repositories.Ranking.Contracts.Models;

public record ForSaleRankingModel
{
    public required string Name { get; init; }
    public required int ForSaleCount { get; init; }
}