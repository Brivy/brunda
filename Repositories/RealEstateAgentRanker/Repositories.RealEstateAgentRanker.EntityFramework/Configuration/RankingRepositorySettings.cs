using System.ComponentModel.DataAnnotations;

namespace Funda.Assigment.Repositories.RealEstateAgentRanker.EntityFramework.Configuration;

internal record RankingRepositorySettings
{
    [Required]
    public required string ConnectionString { get; init; }
}
