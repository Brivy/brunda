using Funda.Assigment.Repositories.RealEstateAgentRanker.Contracts.Models;

namespace Funda.Assigment.Repositories.RealEstateAgentRanker.Contracts.Repositories;

public interface IRankingRepository
{
    Task RefreshForSaleRankingAsync(IReadOnlyCollection<ForSaleRankingModel> forSaleRankings, CancellationToken cancellationToken);
    Task RefreshForSaleWithGardenRankingAsync(IReadOnlyCollection<ForSaleRankingModel> forSaleWithGardenRankings, CancellationToken cancellationToken);
}