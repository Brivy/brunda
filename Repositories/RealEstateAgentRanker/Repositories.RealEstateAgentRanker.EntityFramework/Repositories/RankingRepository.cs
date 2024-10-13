using Funda.Assigment.Repositories.RealEstateAgentRanker.Contracts.Models;
using Funda.Assigment.Repositories.RealEstateAgentRanker.Contracts.Repositories;
using Funda.Assigment.Repositories.RealEstateAgentRanker.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace Funda.Assigment.Repositories.RealEstateAgentRanker.EntityFramework.Repositories;

internal class RankingRepository(RankingContext context) : IRankingRepository
{
    private readonly RankingContext _context = context;

    public async Task RefreshForSaleRankingAsync(IReadOnlyCollection<ForSaleRankingModel> forSaleRankings, CancellationToken cancellationToken)
    {
        if (forSaleRankings.Count == 0)
        {
            return;
        }

        var existingRankings = await _context.ForSaleRankings.ToListAsync(cancellationToken).ConfigureAwait(false);
        _context.RemoveRange(existingRankings);

        var orderedRealEstateAgents = forSaleRankings
            .OrderByDescending(x => x.ForSaleCount)
            .Take(10)
            .Select(x => new ForSaleRanking
            {
                RealEstateAgentName = x.Name,
                ForSaleCount = x.ForSaleCount
            })
            .ToList();

        _context.AddRange(orderedRealEstateAgents);
        _ = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task RefreshForSaleWithGardenRankingAsync(IReadOnlyCollection<ForSaleRankingModel> forSaleWithGardenRankings, CancellationToken cancellationToken)
    {
        if (forSaleWithGardenRankings.Count == 0)
        {
            return;
        }

        var existingRankings = await _context.ForSaleWithGardenRankings.ToListAsync(cancellationToken).ConfigureAwait(false);
        _context.RemoveRange(existingRankings);

        var orderedRealEstateAgents = forSaleWithGardenRankings
            .OrderByDescending(x => x.ForSaleCount)
            .Take(10)
            .Select(x => new ForSaleWithGardenRanking
            {
                RealEstateAgentName = x.Name,
                ForSaleCount = x.ForSaleCount
            })
            .ToList();

        _context.AddRange(orderedRealEstateAgents);
        _ = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
