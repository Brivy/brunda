namespace Funda.Assigment.Repositories.Ranking.Contracts.Models;

public record ForSaleWithGardenRankingModel
{
    public required string Name { get; init; }
    public required int ForSaleCount { get; init; }
}