namespace Brunda.Modules.Ranking.Contracts;

public interface IRealEstateAgentRanker
{
    Task RankAsync(CancellationToken cancellationToken);
    Task RankWithGardenAsync(CancellationToken cancellationToken);
}