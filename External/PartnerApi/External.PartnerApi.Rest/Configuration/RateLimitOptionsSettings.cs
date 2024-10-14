using System.ComponentModel.DataAnnotations;

namespace Brunda.External.PartnerApi.Rest.Configuration;

internal record RateLimitOptionsSettings
{
    [Required]
    public required int PermitLimit { get; init; }
    [Required]
    public required TimeSpan LimitWindow { get; init; }
}
