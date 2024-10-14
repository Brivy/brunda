using Funda.Assigment.External.PartnerApi.Contracts.Models;

namespace Funda.Assigment.Business.Ranking.Contracts;

internal interface IRealEstateAgentService
{
    Task<IReadOnlyCollection<RealEstateAgentSummaryModel>> GetSummariesAsync(string location, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<RealEstateAgentSummaryModel>> GetSummariesAsync(string location, bool hasGarden, CancellationToken cancellationToken);
}