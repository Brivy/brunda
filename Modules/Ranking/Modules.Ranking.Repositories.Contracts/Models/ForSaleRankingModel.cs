namespace Brunda.Modules.Ranking.Repositories.Contracts.Models;

public record ForSaleRankingModel
{
    public required string Name { get; init; }
    public required int ForSaleCount { get; init; }
}