using System.ComponentModel.DataAnnotations;

namespace Brunda.Repositories.Ranking.EntityFramework.Configuration;

internal record RankingRepositorySettings
{
    [Required]
    public required string ConnectionString { get; init; }
}
