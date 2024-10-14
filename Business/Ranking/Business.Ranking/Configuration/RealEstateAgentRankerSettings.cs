using System.ComponentModel.DataAnnotations;

namespace Brunda.Business.Ranking.Configuration;

internal record RealEstateAgentRankerSettings
{
    [Required]
    public required string SearchLocation { get; init; }
}
