using Brunda.Modules.Ranking.Repositories.Contracts.Models;

namespace Brunda.Modules.Ranking.Repositories.Contracts.Repositories
{
    public interface IForSaleRankingRepository
    {
        Task ClearRankingAsync(CancellationToken cancellationToken);
        Task CreateRankingAsync(IReadOnlyCollection<ForSaleRankingModel> forSaleRankings, CancellationToken cancellationToken);
    }
}