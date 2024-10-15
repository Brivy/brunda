using Brunda.Modules.Ranking.Configuration;

namespace Brunda.Modules.Ranking.Tests.Configuration;

internal class RealEstateAgentRankerSettingsBuilder
{
    private string _searchLocation = string.Empty;

    public RealEstateAgentRankerSettingsBuilder WithSearchLocation(string searchLocation)
    {
        _searchLocation = searchLocation;
        return this;
    }

    public RealEstateAgentRankerSettings Build()
    {
        return new RealEstateAgentRankerSettings
        {
            SearchLocation = _searchLocation
        };
    }
}
