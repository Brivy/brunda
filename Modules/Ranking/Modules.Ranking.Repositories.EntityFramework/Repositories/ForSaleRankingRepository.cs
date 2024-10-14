using Brunda.Modules.Ranking.Repositories.Contracts.Models;
using Brunda.Modules.Ranking.Repositories.Contracts.Repositories;
using Brunda.Modules.Ranking.Repositories.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace Brunda.Modules.Ranking.Repositories.EntityFramework.Repositories;

internal class ForSaleRankingRepository(RankingContext context) : IForSaleRankingRepository
{
    private readonly RankingContext _context = context;

    public async Task ClearRankingAsync(CancellationToken cancellationToken)
    {
        var existingRankings = await _context.ForSaleRankings.ToListAsync(cancellationToken).ConfigureAwait(false);
        if (existingRankings.Count == 0)
        {
            return;
        }

        _context.RemoveRange(existingRankings);
        _ = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task CreateRankingAsync(IReadOnlyCollection<ForSaleRankingModel> forSaleRankings, CancellationToken cancellationToken)
    {
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
}
