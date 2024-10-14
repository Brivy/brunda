using Brunda.Modules.Ranking.Repositories.Contracts.Models;

namespace Brunda.Modules.Ranking.Contracts;

internal interface IForSaleRankingService
{
    Task RankAsync(IReadOnlyCollection<ForSaleRankingModel> forSaleRankings, CancellationToken cancellationToken);
}