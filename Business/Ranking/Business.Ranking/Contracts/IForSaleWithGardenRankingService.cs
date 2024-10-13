using Funda.Assigment.Repositories.Ranking.Contracts.Models;

namespace Funda.Assigment.Business.Ranking.Contracts;

internal interface IForSaleWithGardenRankingService
{
    Task RankAsync(IReadOnlyCollection<ForSaleWithGardenRankingModel> forSaleWithGardenRankings, CancellationToken cancellationToken);
}