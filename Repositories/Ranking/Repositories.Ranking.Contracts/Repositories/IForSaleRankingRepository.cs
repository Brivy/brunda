using Brunda.Repositories.Ranking.Contracts.Models;

namespace Brunda.Repositories.Ranking.Contracts.Repositories
{
    public interface IForSaleRankingRepository
    {
        Task ClearRankingAsync(CancellationToken cancellationToken);
        Task CreateRankingAsync(IReadOnlyCollection<ForSaleRankingModel> forSaleRankings, CancellationToken cancellationToken);
    }
}