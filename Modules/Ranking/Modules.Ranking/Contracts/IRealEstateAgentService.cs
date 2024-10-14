using Brunda.External.PartnerApi.Contracts.Models;

namespace Brunda.Modules.Ranking.Contracts;

internal interface IRealEstateAgentService
{
    Task<IReadOnlyCollection<RealEstateAgentSummaryModel>> GetSummariesAsync(string location, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<RealEstateAgentSummaryModel>> GetSummariesAsync(string location, bool hasGarden, CancellationToken cancellationToken);
}