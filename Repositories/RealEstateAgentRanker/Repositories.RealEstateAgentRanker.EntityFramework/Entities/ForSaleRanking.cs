namespace Funda.Assigment.Repositories.RealEstateAgentRanker.EntityFramework.Entities;

internal class ForSaleRanking
{
    public int Id { get; set; }
    public required string RealEstateAgentName { get; set; }
    public int ForSaleCount { get; set; }
}
