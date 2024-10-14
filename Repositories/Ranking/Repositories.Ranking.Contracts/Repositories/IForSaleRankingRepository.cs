using Funda.Assigment.Repositories.Ranking.Contracts.Models;

namespace Funda.Assigment.Repositories.Ranking.Contracts.Repositories
{
    public interface IForSaleRankingRepository
    {
        Task ClearRankingAsync(CancellationToken cancellationToken);
        Task CreateRankingAsync(IReadOnlyCollection<ForSaleRankingModel> forSaleRankings, CancellationToken cancellationToken);
    }
}