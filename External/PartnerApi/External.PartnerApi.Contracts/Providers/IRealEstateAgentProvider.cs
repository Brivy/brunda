using Brunda.External.PartnerApi.Contracts.Models;
using Brunda.External.PartnerApi.Contracts.Options;

namespace Brunda.External.PartnerApi.Contracts.Providers;

public interface IRealEstateAgentProvider
{
    Task<PageModel<RealEstateAgentSummaryModel>?> GetSummaryDataAsync(OfferOptions offerOptions, CancellationToken cancellationToken);
}
