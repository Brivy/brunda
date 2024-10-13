using System.ComponentModel.DataAnnotations;

namespace Funda.Assigment.Business.Ranking.Configuration;

internal record RealEstateAgentRankerSettings
{
    [Required]
    public required string SearchLocation { get; init; }
}
