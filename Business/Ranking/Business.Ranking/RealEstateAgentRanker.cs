using Brunda.Business.Ranking.Configuration;
using Brunda.Business.Ranking.Contracts;
using Brunda.Repositories.Ranking.Contracts.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Brunda.Business.Ranking;

internal class RealEstateAgentRanker(
    IForSaleRankingService forSaleRankingService,
    IForSaleWithGardenRankingService forSaleWithGardenRankingService,
    IRealEstateAgentService realEstateAgentService,
    IOptions<RealEstateAgentRankerSettings> options,
    ILogger<RealEstateAgentRanker> logger) : IRealEstateAgentRanker
{
    private readonly IForSaleRankingService _forSaleRankingService = forSaleRankingService;
    private readonly IForSaleWithGardenRankingService _forSaleWithGardenRankingService = forSaleWithGardenRankingService;
    private readonly IRealEstateAgentService _realEstateAgentService = realEstateAgentService;
    private readonly RealEstateAgentRankerSettings _settings = options.Value;
    private readonly ILogger<RealEstateAgentRanker> _logger = logger;

    public async Task RankAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Ranking real estate agents by properties for sale...");

        var realEstateAgentSummaries = await _realEstateAgentService.GetSummariesAsync(_settings.SearchLocation, cancellationToken).ConfigureAwait(false);
        if (realEstateAgentSummaries.Count == 0)
        {
            _logger.LogInformation("No real estate agents found in the area");
            return;
        }

        var forSaleRankings = realEstateAgentSummaries.Select(x => new ForSaleRankingModel
        {
            ForSaleCount = x.ForSaleCount,
            Name = x.RealEstateAgentName
        }).ToList();

        await _forSaleRankingService.RankAsync(forSaleRankings, cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("Ranking ended...");
    }

    public async Task RankWithGardenAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Ranking real estate agents by properties with a garden for sale");

        var realEstateAgentSummaries = await _realEstateAgentService.GetSummariesAsync(_settings.SearchLocation, true, cancellationToken).ConfigureAwait(false);
        if (realEstateAgentSummaries.Count == 0)
        {
            _logger.LogInformation("No real estate agents found in the area that are dealing with gardens");
            return;
        }

        var forSaleWithGardenRankings = realEstateAgentSummaries.Select(x => new ForSaleWithGardenRankingModel
        {
            ForSaleCount = x.ForSaleCount,
            Name = x.RealEstateAgentName
        }).ToList();

        await _forSaleWithGardenRankingService.RankAsync(forSaleWithGardenRankings, cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("Ranking ended...");
    }
}
