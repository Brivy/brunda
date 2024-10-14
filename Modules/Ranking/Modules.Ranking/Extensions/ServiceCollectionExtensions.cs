using Brunda.Configuration.Common.Extensions;
using Brunda.Modules.Ranking.Configuration;
using Brunda.Modules.Ranking.Constants;
using Brunda.Modules.Ranking.Contracts;
using Brunda.Modules.Ranking.Repositories.EntityFramework.Extensions;
using Brunda.Modules.Ranking.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Brunda.Modules.Ranking.Extensions;

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
