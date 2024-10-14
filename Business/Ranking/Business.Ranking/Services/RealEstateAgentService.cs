using Brunda.Business.Ranking.Contracts;
using Brunda.External.PartnerApi.Contracts.Constants;
using Brunda.External.PartnerApi.Contracts.Enums;
using Brunda.External.PartnerApi.Contracts.Models;
using Brunda.External.PartnerApi.Contracts.Options;
using Brunda.External.PartnerApi.Contracts.Providers;
using Microsoft.Extensions.Logging;

namespace Brunda.Business.Ranking.Services;

internal class RealEstateAgentService(
    IRealEstateAgentProvider realEstateAgentProvider,
    ILogger<RealEstateAgentService> logger) : IRealEstateAgentService
{
    private readonly IRealEstateAgentProvider _realEstateAgentProvider = realEstateAgentProvider;
    private readonly ILogger<RealEstateAgentService> _logger = logger;

    public Task<IReadOnlyCollection<RealEstateAgentSummaryModel>> GetSummariesAsync(string location, CancellationToken cancellationToken) =>
        GetSummariesAsync(location, false, cancellationToken);

    public async Task<IReadOnlyCollection<RealEstateAgentSummaryModel>> GetSummariesAsync(string location, bool hasGarden, CancellationToken cancellationToken)
    {
        var currentPage = 1;
        int pagesLeft;

        var realEstateAgentSummaries = new List<RealEstateAgentSummaryModel>();
        do
        {
            _logger.LogInformation("Trying to retrieve results from page {CurrentPage} of the PartnerApi", currentPage);

            var offerOptions = new OfferOptions
            {
                Location = location,
                HasGarden = hasGarden,
                Page = currentPage,
                PageSize = PartnerApiConstants.MaxPageSize,
                ResidenceContractType = ResidenceContractType.Buy
            };

            var result = await _realEstateAgentProvider.GetSummaryDataAsync(offerOptions, cancellationToken).ConfigureAwait(false);
            if (result == null)
            {
                _logger.LogWarning("The partner API returned an invalid response for page {CurrentPage} that couldn't be resolved", currentPage);
                break;
            }

            realEstateAgentSummaries.AddRange(result.Results);
            pagesLeft = result.TotalPages - result.CurrentPage;
            currentPage = result.CurrentPage + 1;
        } while (pagesLeft > 0);

        return MergeSummaries(realEstateAgentSummaries);
    }

    private static List<RealEstateAgentSummaryModel> MergeSummaries(IReadOnlyCollection<RealEstateAgentSummaryModel> summaries) =>
        summaries.GroupBy(x => x.RealEstateAgentId)
            .Select(x => new RealEstateAgentSummaryModel
            {
                ForSaleCount = x.Sum(y => y.ForSaleCount),
                RealEstateAgentId = x.Key,
                RealEstateAgentName = x.First().RealEstateAgentName
            }).ToList();
}
