using Brunda.External.PartnerApi.Contracts.Options;
using Brunda.External.PartnerApi.Rest.QueryParameters;

namespace Brunda.External.PartnerApi.Rest.Extensions;

internal static class OfferOptionsModelExtensions
{
    public static OfferQueryParameters ToQueryParameters(this OfferOptions offerOptions)
    {
        var searchQuery = !string.IsNullOrWhiteSpace(offerOptions.Location)
            ? $"/{offerOptions.Location}/"
            : null;

        if (searchQuery != null && offerOptions.HasGarden)
        {
            searchQuery += "tuin/";
        }

        return new OfferQueryParameters
        {
            ResidenceContractType = offerOptions.ResidenceContractType.ToQueryParameterName(),
            SearchQuery = searchQuery,
            Page = offerOptions.Page,
            PageSize = offerOptions.PageSize
        };
    }
}
