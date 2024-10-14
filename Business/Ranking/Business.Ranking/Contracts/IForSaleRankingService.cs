using Brunda.Repositories.Ranking.Contracts.Models;

namespace Brunda.Business.Ranking.Contracts;

internal interface IForSaleRankingService
{
    Task RankAsync(IReadOnlyCollection<ForSaleRankingModel> forSaleRankings, CancellationToken cancellationToken);
}