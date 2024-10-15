using Brunda.External.PartnerApi.Contracts.Models;

namespace Brunda.External.PartnerApi.Tests.Builder.Models;

public class RealEstateAgentSummaryModelBuilder
{
    private int _realEstateAgentId = default;
    private string _realEstateAgentName = string.Empty;
    private int _forSaleCount = default;

    public RealEstateAgentSummaryModelBuilder WithRealEstateAgentId(int realEstateAgentId)
    {
        _realEstateAgentId = realEstateAgentId;
        return this;
    }

    public RealEstateAgentSummaryModelBuilder WithRealEstateAgentName(string realEstateAgentName)
    {
        _realEstateAgentName = realEstateAgentName;
        return this;
    }

    public RealEstateAgentSummaryModelBuilder WithForSaleCount(int forSaleCount)
    {
        _forSaleCount = forSaleCount;
        return this;
    }

    public RealEstateAgentSummaryModel Build()
    {
        return new RealEstateAgentSummaryModel
        {
            RealEstateAgentId = _realEstateAgentId,
            RealEstateAgentName = _realEstateAgentName,
            ForSaleCount = _forSaleCount
        };
    }
}
