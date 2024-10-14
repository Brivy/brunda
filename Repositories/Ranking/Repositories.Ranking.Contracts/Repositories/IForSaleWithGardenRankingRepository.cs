using Brunda.Repositories.Ranking.Contracts.Models;

namespace Brunda.Repositories.Ranking.Contracts.Repositories
{
    public interface IForSaleWithGardenRankingRepository
    {
        Task ClearRankingAsync(CancellationToken cancellationToken);
        Task CreateRankingAsync(IReadOnlyCollection<ForSaleWithGardenRankingModel> forSaleWithGardenRankings, CancellationToken cancellationToken);
    }
}