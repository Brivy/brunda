using Brunda.Modules.Ranking.Repositories.Contracts.Models;

namespace Modules.Ranking.Repositories.Tests.Builder.Models;

public class ForSaleRankingModelBuilder
{
    private string _name = string.Empty;
    private int _forSaleCount = default;

    public ForSaleRankingModelBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public ForSaleRankingModelBuilder WithForSaleCount(int forSaleCount)
    {
        _forSaleCount = forSaleCount;
        return this;
    }

    public ForSaleRankingModel Build()
    {
        return new ForSaleRankingModel
        {
            Name = _name,
            ForSaleCount = _forSaleCount
        };
    }
}
