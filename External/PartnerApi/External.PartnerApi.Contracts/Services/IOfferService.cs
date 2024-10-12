using Funda.Assigment.External.PartnerApi.Contracts.Models;
using Funda.Assigment.External.PartnerApi.Contracts.Options;

namespace Funda.Assigment.External.PartnerApi.Contracts.Services;

public interface IOfferService
{
    Task<IReadOnlyCollection<RealEstateAgentSummaryModel>> GetRealEstateAgentSummaryAsync(OfferOptionsModel offerOptions, CancellationToken cancellationToken);
}
