using Brunda.External.PartnerApi.Contracts.Enums;
using Brunda.External.PartnerApi.Contracts.Options;

namespace Brunda.External.PartnerApi.Tests.Builder.Options;

public class OfferOptionsBuilder
{
    private ResidenceContractType _residenceContractType = ResidenceContractType.Unknown;
    private string? _location = default;
    private bool _hasGarden = default;
    private int? _page = default;
    private int? _pageSize = default;

    public OfferOptionsBuilder WithResidenceContractType(ResidenceContractType residenceContractType)
    {
        _residenceContractType = residenceContractType;
        return this;
    }

    public OfferOptionsBuilder WithLocation(string? location)
    {
        _location = location;
        return this;
    }

    public OfferOptionsBuilder WithHasGarden(bool hasGarden)
    {
        _hasGarden = hasGarden;
        return this;
    }

    public OfferOptionsBuilder WithPage(int? page)
    {
        _page = page;
        return this;
    }

    public OfferOptionsBuilder WithPageSize(int? pageSize)
    {
        _pageSize = pageSize;
        return this;
    }

    public OfferOptions Build()
    {
        return new OfferOptions
        {
            ResidenceContractType = _residenceContractType,
            Location = _location,
            HasGarden = _hasGarden,
            Page = _page,
            PageSize = _pageSize
        };
    }
}
