using System.ComponentModel.DataAnnotations;

namespace Funda.Assigment.External.PartnerApi.Rest.Configuration;

internal record RetryOptionsSettings
{
    [Required]
    public required TimeSpan Delay { get; init; }
    [Required]
    public required int MaxRetryAttempts { get; init; }
}
