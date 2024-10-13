using Funda.Assigment.Repositories.Ranking.Contracts.Models;
using Funda.Assigment.Repositories.Ranking.Contracts.Repositories;
using Funda.Assigment.Repositories.Ranking.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace Funda.Assigment.Repositories.Ranking.EntityFramework.Repositories;

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
