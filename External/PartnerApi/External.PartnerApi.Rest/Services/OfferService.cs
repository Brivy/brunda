using Funda.Assigment.External.PartnerApi.Contracts.Models;
using Funda.Assigment.External.PartnerApi.Contracts.Options;
using Funda.Assigment.External.PartnerApi.Contracts.Services;
using Funda.Assigment.External.PartnerApi.Rest.Clients;
using Funda.Assigment.External.PartnerApi.Rest.Configuration;
using Funda.Assigment.External.PartnerApi.Rest.Constants;
using Funda.Assigment.External.PartnerApi.Rest.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly.Registry;
using Refit;

namespace Funda.Assigment.External.PartnerApi.Rest.Services;

internal class OfferService(
    IPartnerApiClient partnerApiClient,
    ResiliencePipelineProvider<string> resiliencePipelineProvider,
    IOptionsSnapshot<PartnerApiSettings> options,
    ILogger<OfferService> logger) : IOfferService
{
    private readonly IPartnerApiClient _partnerApiClient = partnerApiClient;
    private readonly ResiliencePipelineProvider<string> _resiliencePipelineProvider = resiliencePipelineProvider;
    private readonly PartnerApiSettings _settings = options.Value;
    private readonly ILogger<OfferService> _logger = logger;

    public async Task<IReadOnlyCollection<RealEstateAgentSummaryModel>> GetRealEstateAgentSummaryAsync(OfferOptionsModel offerOptions, CancellationToken cancellationToken)
    {
        var queryParameters = offerOptions.ToQueryParameters();
        return await _resiliencePipelineProvider.GetPipeline(ResiliencePipelineConstants.PartnerApiKey)
            .ExecuteAsync(async (t, token) =>
            {
                _logger.LogInformation("Getting offers from partner API with query parameters: {QueryParameters}", queryParameters);

                var summaries = new List<RealEstateAgentSummaryModel>();
                try
                {
                    var result = await _partnerApiClient.GetOffersAsync(_settings.ApiKey, queryParameters, cancellationToken).ConfigureAwait(false);
                    if (!result.IsSuccessStatusCode || result.Content == null)
                    {
                        _logger.LogWarning("Received an invalid status code from the partner API: {StatusCode}", result.StatusCode);
                        return summaries;
                    }

                    return result.Content.Residences
                        .GroupBy(x => x.RealEstateAgentId)
                        .Select(x => new RealEstateAgentSummaryModel
                        {
                            RealEstateAgentId = x.Key,
                            RealEstateAgentName = x.First().RealEstateAgentName,
                            ResidencesForSaleCount = x.Count()
                        }).ToList();
                }
                catch (ApiException ex)
                {
                    _logger.LogError(ex, "Failed to get offers from partner API with query parameters: {QueryParameters}", queryParameters);
                    return summaries;
                }
            }, cancellationToken).ConfigureAwait(false);
    }
}
