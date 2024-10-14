using Brunda.External.PartnerApi.Contracts.Models;
using Brunda.External.PartnerApi.Contracts.Options;
using Brunda.External.PartnerApi.Contracts.Providers;
using Brunda.External.PartnerApi.Rest.Clients;
using Brunda.External.PartnerApi.Rest.Configuration;
using Brunda.External.PartnerApi.Rest.Constants;
using Brunda.External.PartnerApi.Rest.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly.Registry;
using Refit;

namespace Brunda.External.PartnerApi.Rest.Providers;

internal class RealEstateAgentProvider(
    IPartnerApiClient partnerApiClient,
    ResiliencePipelineProvider<string> resiliencePipelineProvider,
    IOptionsSnapshot<PartnerApiSettings> options,
    ILogger<RealEstateAgentProvider> logger) : IRealEstateAgentProvider
{
    private readonly IPartnerApiClient _partnerApiClient = partnerApiClient;
    private readonly ResiliencePipelineProvider<string> _resiliencePipelineProvider = resiliencePipelineProvider;
    private readonly PartnerApiSettings _settings = options.Value;
    private readonly ILogger<RealEstateAgentProvider> _logger = logger;

    public async Task<PageModel<RealEstateAgentSummaryModel>?> GetSummaryDataAsync(OfferOptions offerOptions, CancellationToken cancellationToken)
    {
        var queryParameters = offerOptions.ToQueryParameters();
        return await _resiliencePipelineProvider.GetPipeline(ResiliencePipelineConstants.PartnerApiKey)
            .ExecuteAsync(async (t, token) =>
            {
                _logger.LogInformation("Getting real estate agent data from partner API with query parameters: {QueryParameters}", queryParameters);

                try
                {
                    var result = await _partnerApiClient.GetOffersAsync(_settings.ApiKey, queryParameters, cancellationToken).ConfigureAwait(false);
                    if (!result.IsSuccessStatusCode || result.Content == null)
                    {
                        _logger.LogWarning("Received an invalid status code from the partner API: {StatusCode}", result.StatusCode);
                        return null;
                    }

                    var summaries = result.Content.Residences
                        .GroupBy(x => x.RealEstateAgentId)
                        .Select(x => new RealEstateAgentSummaryModel
                        {
                            RealEstateAgentId = x.Key,
                            RealEstateAgentName = x.First().RealEstateAgentName,
                            ForSaleCount = x.Count()
                        }).ToList();

                    return new PageModel<RealEstateAgentSummaryModel>
                    {
                        CurrentPage = result.Content.Paging.CurrentPage,
                        Results = summaries,
                        TotalPages = result.Content.Paging.TotalPages
                    };
                }
                catch (ApiException ex)
                {
                    _logger.LogError(ex, "Failed to get real estate agent data from partner API with query parameters: {QueryParameters}", queryParameters);
                    return null;
                }
            }, cancellationToken).ConfigureAwait(false);
    }
}
