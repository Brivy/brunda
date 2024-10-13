using Funda.Assigment.External.PartnerApi.Rest.QueryParameters;
using Funda.Assigment.External.PartnerApi.Rest.Responses;
using Refit;

namespace Funda.Assigment.External.PartnerApi.Rest.Clients;

internal interface IPartnerApiClient
{
    [Get("/{apiKey}")]
    Task<ApiResponse<OfferResponse>> GetOffersAsync(string apiKey, OfferQueryParameters queryParameters, CancellationToken cancellationToken);
}
