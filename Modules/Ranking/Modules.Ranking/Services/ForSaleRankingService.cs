﻿using Brunda.Modules.Ranking.Contracts;
using Brunda.Modules.Ranking.Repositories.Contracts.Models;
using Brunda.Modules.Ranking.Repositories.Contracts.Repositories;
using Microsoft.Extensions.Logging;

namespace Brunda.Modules.Ranking.Services;

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
