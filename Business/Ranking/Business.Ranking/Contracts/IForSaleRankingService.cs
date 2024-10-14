using Funda.Assigment.Repositories.Ranking.Contracts.Models;

namespace Funda.Assigment.Business.Ranking.Contracts;

internal interface IForSaleRankingService
{
    Task RankAsync(IReadOnlyCollection<ForSaleRankingModel> forSaleRankings, CancellationToken cancellationToken);
}