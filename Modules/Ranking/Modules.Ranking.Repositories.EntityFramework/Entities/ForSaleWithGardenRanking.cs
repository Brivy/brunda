namespace Brunda.Modules.Ranking.Repositories.EntityFramework.Entities;

internal class ForSaleWithGardenRanking
{
    public int Id { get; set; }
    public required string RealEstateAgentName { get; set; }
    public int ForSaleCount { get; set; }
}
