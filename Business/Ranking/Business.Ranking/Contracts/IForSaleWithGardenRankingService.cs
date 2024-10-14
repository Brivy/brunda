using Brunda.Repositories.Ranking.Contracts.Models;

namespace Brunda.Business.Ranking.Contracts;

internal interface IForSaleWithGardenRankingService
{
    Task RankAsync(IReadOnlyCollection<ForSaleWithGardenRankingModel> forSaleWithGardenRankings, CancellationToken cancellationToken);
}