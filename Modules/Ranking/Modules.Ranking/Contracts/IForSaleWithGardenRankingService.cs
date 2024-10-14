using Brunda.Modules.Ranking.Repositories.Contracts.Models;

namespace Brunda.Modules.Ranking.Contracts;

internal interface IForSaleWithGardenRankingService
{
    Task RankAsync(IReadOnlyCollection<ForSaleWithGardenRankingModel> forSaleWithGardenRankings, CancellationToken cancellationToken);
}