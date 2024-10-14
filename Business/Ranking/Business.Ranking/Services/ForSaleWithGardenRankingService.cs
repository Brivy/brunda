﻿using Brunda.Business.Ranking.Contracts;
using Brunda.Repositories.Ranking.Contracts.Models;
using Brunda.Repositories.Ranking.Contracts.Repositories;
using Microsoft.Extensions.Logging;

namespace Brunda.Business.Ranking.Services;

internal class ForSaleWithGardenRankingService(
    IForSaleWithGardenRankingRepository forSaleWithGardenRankingRepository,
    ILogger<ForSaleWithGardenRankingService> logger) : IForSaleWithGardenRankingService
{
    private readonly IForSaleWithGardenRankingRepository _forSaleWithGardenRankingRepository = forSaleWithGardenRankingRepository;
    private readonly ILogger<ForSaleWithGardenRankingService> _logger = logger;

    public async Task RankAsync(IReadOnlyCollection<ForSaleWithGardenRankingModel> forSaleWithGardenRankings, CancellationToken cancellationToken)
    {
        try
        {
            await _forSaleWithGardenRankingRepository.ClearRankingAsync(cancellationToken).ConfigureAwait(false);
            await _forSaleWithGardenRankingRepository.CreateRankingAsync(forSaleWithGardenRankings, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong while trying to rank residences (that have a garden) for sale");
        }
    }
}