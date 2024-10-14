namespace Brunda.Modules.Ranking.Repositories.EntityFramework.Entities;

internal class ForSaleRanking
{
    public int Id { get; set; }
    public required string RealEstateAgentName { get; set; }
    public int ForSaleCount { get; set; }
}
