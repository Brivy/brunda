using Brunda.Business.Ranking.Configuration;
using Brunda.Business.Ranking.Constants;
using Brunda.Business.Ranking.Contracts;
using Brunda.Business.Ranking.Services;
using Brunda.Configuration.Common.Extensions;
using Brunda.Repositories.Ranking.EntityFramework.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Brunda.Business.Ranking.Extensions;

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
