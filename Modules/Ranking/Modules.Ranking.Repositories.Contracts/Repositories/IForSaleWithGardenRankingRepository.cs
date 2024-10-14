using Brunda.Modules.Ranking.Repositories.Contracts.Models;

namespace Brunda.Modules.Ranking.Repositories.Contracts.Repositories
{
    public interface IForSaleWithGardenRankingRepository
    {
        Task ClearRankingAsync(CancellationToken cancellationToken);
        Task CreateRankingAsync(IReadOnlyCollection<ForSaleWithGardenRankingModel> forSaleWithGardenRankings, CancellationToken cancellationToken);
    }
}