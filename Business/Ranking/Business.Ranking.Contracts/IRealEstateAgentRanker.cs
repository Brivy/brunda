namespace Funda.Assigment.Business.Ranking.Contracts;

public interface IRealEstateAgentRanker
{
    Task RankAsync(CancellationToken cancellationToken);
    Task RankWithGardenAsync(CancellationToken cancellationToken);
}