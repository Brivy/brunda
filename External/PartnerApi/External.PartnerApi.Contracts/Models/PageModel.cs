namespace Brunda.External.PartnerApi.Contracts.Models;

public record PageModel<TResult>
{
    public required IReadOnlyCollection<TResult> Results { get; init; } = [];
    public required int CurrentPage { get; init; }
    public required int TotalPages { get; init; }
}
