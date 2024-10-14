using Brunda.Repositories.Ranking.Contracts.Models;
using Brunda.Repositories.Ranking.Contracts.Repositories;
using Brunda.Repositories.Ranking.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace Brunda.Repositories.Ranking.EntityFramework.Repositories;

internal class ForSaleWithGardenRankingRepository(RankingContext context) : IForSaleWithGardenRankingRepository
{
    private readonly RankingContext _context = context;

    public async Task ClearRankingAsync(CancellationToken cancellationToken)
    {
        var existingRankings = await _context.ForSaleWithGardenRankings.ToListAsync(cancellationToken).ConfigureAwait(false);
        if (existingRankings.Count == 0)
        {
            return;
        }

        _context.RemoveRange(existingRankings);
        _ = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task CreateRankingAsync(IReadOnlyCollection<ForSaleWithGardenRankingModel> forSaleWithGardenRankings, CancellationToken cancellationToken)
    {
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
