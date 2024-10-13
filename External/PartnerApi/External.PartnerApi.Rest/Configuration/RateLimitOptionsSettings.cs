using System.ComponentModel.DataAnnotations;

namespace Funda.Assigment.External.PartnerApi.Rest.Configuration;

internal record RateLimitOptionsSettings
{
    [Required]
    public required int PermitLimt { get; init; }
    [Required]
    public required TimeSpan LimtiWindow { get; init; }
}
