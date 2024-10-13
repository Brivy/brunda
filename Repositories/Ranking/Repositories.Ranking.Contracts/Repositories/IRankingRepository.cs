using Funda.Assigment.Repositories.Ranking.Contracts.Models;

namespace Funda.Assigment.Repositories.Ranking.Contracts.Repositories;

public interface IRankingRepository
{
    Task RefreshForSaleRankingAsync(IReadOnlyCollection<ForSaleRankingModel> forSaleRankings, CancellationToken cancellationToken);
    Task RefreshForSaleWithGardenRankingAsync(IReadOnlyCollection<ForSaleRankingModel> forSaleWithGardenRankings, CancellationToken cancellationToken);
}