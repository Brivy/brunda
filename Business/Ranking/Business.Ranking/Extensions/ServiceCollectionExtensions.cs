using Funda.Assigment.Business.Ranking.Configuration;
using Funda.Assigment.Business.Ranking.Constants;
using Funda.Assigment.Business.Ranking.Contracts;
using Funda.Assigment.Business.Ranking.Services;
using Funda.Assigment.Configuration.Common.Extensions;
using Funda.Assigment.Repositories.Ranking.EntityFramework.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Funda.Assigment.Business.Ranking.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRankingServices(this IServiceCollection services, IConfiguration configuration)
    {
        var rankingConfiguration = configuration.GetRequiredSection(ConfigurationConstants.RankingSectionKey);
        var realEstateAgentRankerConfiguration = rankingConfiguration.GetRequiredSection(ConfigurationConstants.RealEstateAgentRankerSectionKey);

        return services
            .AddOptionsWithValidation<RealEstateAgentRankerSettings>(realEstateAgentRankerConfiguration)
            .AddRankingRepositories(rankingConfiguration)
            .AddScoped<IForSaleRankingService, ForSaleRankingService>()
            .AddScoped<IForSaleWithGardenRankingService, ForSaleWithGardenRankingService>()
            .AddScoped<IRealEstateAgentService, RealEstateAgentService>()
            .AddScoped<IRealEstateAgentRanker, RealEstateAgentRanker>();
    }
}
