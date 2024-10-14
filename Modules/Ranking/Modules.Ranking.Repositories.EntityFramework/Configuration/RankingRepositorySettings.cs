using System.ComponentModel.DataAnnotations;

namespace Brunda.Modules.Ranking.Repositories.EntityFramework.Configuration;

internal record RankingRepositorySettings
{
    [Required]
    public required string ConnectionString { get; init; }
}
