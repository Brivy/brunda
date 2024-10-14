using Funda.Assigment.Repositories.Ranking.Contracts.Models;

namespace Funda.Assigment.Repositories.Ranking.Contracts.Repositories
{
    public interface IForSaleWithGardenRankingRepository
    {
        Task ClearRankingAsync(CancellationToken cancellationToken);
        Task CreateRankingAsync(IReadOnlyCollection<ForSaleWithGardenRankingModel> forSaleWithGardenRankings, CancellationToken cancellationToken);
    }
}