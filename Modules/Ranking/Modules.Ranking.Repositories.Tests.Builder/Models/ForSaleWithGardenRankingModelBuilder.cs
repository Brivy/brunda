using Brunda.Modules.Ranking.Repositories.Contracts.Models;

namespace Modules.Ranking.Repositories.Tests.Builder.Models;

public class ForSaleWithGardenRankingModelBuilder
{
    private string _name = string.Empty;
    private int _forSaleCount = default;

    public ForSaleWithGardenRankingModelBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public ForSaleWithGardenRankingModelBuilder WithForSaleCount(int forSaleCount)
    {
        _forSaleCount = forSaleCount;
        return this;
    }

    public ForSaleWithGardenRankingModel Build()
    {
        return new ForSaleWithGardenRankingModel
        {
            Name = _name,
            ForSaleCount = _forSaleCount
        };
    }
}
