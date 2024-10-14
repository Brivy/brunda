using System.ComponentModel.DataAnnotations;

namespace Brunda.Modules.Ranking.Configuration;

internal record RealEstateAgentRankerSettings
{
    [Required]
    public required string SearchLocation { get; init; }
}
