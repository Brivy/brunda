using Funda.Assigment.External.PartnerApi.Rest.Models;
using Funda.Assigment.External.PartnerApi.Rest.QueryParameters;
using Refit;

namespace Funda.Assigment.External.PartnerApi.Rest.Clients;

internal interface IPartnerApiClient
{
    [Get("/{apiKey}")]
    Task<ApiResponse<OfferModel>> GetOffersAsync(string apiKey, OfferQueryParameters queryParameters, CancellationToken cancellationToken);
}
