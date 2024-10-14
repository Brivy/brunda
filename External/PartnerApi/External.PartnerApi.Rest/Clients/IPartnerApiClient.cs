using Brunda.External.PartnerApi.Rest.QueryParameters;
using Brunda.External.PartnerApi.Rest.Responses;
using Refit;

namespace Brunda.External.PartnerApi.Rest.Clients;

internal interface IPartnerApiClient
{
    [Get("/{apiKey}")]
    Task<ApiResponse<OfferResponse>> GetOffersAsync(string apiKey, OfferQueryParameters queryParameters, CancellationToken cancellationToken);
}
