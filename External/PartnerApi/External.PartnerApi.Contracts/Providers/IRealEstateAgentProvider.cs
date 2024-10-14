using Funda.Assigment.External.PartnerApi.Contracts.Models;
using Funda.Assigment.External.PartnerApi.Contracts.Options;

namespace Funda.Assigment.External.PartnerApi.Contracts.Providers;

public interface IRealEstateAgentProvider
{
    Task<PageModel<RealEstateAgentSummaryModel>?> GetSummaryDataAsync(OfferOptions offerOptions, CancellationToken cancellationToken);
}
