using Funda.Assigment.Business.Ranking.Contracts;
using Funda.Assigment.Repositories.Ranking.Contracts.Models;
using Funda.Assigment.Repositories.Ranking.Contracts.Repositories;
using Microsoft.Extensions.Logging;

namespace Funda.Assigment.Business.Ranking.Services;

internal class ForSaleRankingService(
    IForSaleRankingRepository forSaleRankingRepository,
    ILogger<ForSaleRankingService> logger) : IForSaleRankingService
{
    private readonly IForSaleRankingRepository _forSaleRankingRepository = forSaleRankingRepository;
    private readonly ILogger<ForSaleRankingService> _logger = logger;

    public async Task RankAsync(IReadOnlyCollection<ForSaleRankingModel> forSaleRankings, CancellationToken cancellationToken)
    {
        try
        {
            await _forSaleRankingRepository.ClearRankingAsync(cancellationToken).ConfigureAwait(false);
            await _forSaleRankingRepository.CreateRankingAsync(forSaleRankings, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong while trying to rank residences for sale");
        }
    }
}
