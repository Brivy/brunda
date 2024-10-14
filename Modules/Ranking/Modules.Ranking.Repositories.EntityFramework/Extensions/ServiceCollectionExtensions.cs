using Brunda.Configuration.Common.Extensions;
using Brunda.Modules.Ranking.Repositories.Contracts.Repositories;
using Brunda.Modules.Ranking.Repositories.EntityFramework;
using Brunda.Modules.Ranking.Repositories.EntityFramework.Configuration;
using Brunda.Modules.Ranking.Repositories.EntityFramework.Constants;
using Brunda.Modules.Ranking.Repositories.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Brunda.Modules.Ranking.Repositories.EntityFramework.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRankingRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        var realEstateAgentRankerRepositoryConfiguration = configuration.GetRequiredSection(ConfigurationConstants.RankingRepositorySectionKey);
        return services
            .AddOptionsWithValidation<RankingRepositorySettings>(realEstateAgentRankerRepositoryConfiguration)
            .AddScoped<IForSaleRankingRepository, ForSaleRankingRepository>()
            .AddScoped<IForSaleWithGardenRankingRepository, ForSaleWithGardenRankingRepository>()
            .AddDbContext<RankingContext>(builder =>
            {
                var realEstateAgentRankerRepositorySettings = realEstateAgentRankerRepositoryConfiguration.Get<RankingRepositorySettings>()
                    ?? throw new InvalidOperationException($"{nameof(RankingRepositorySettings)} not configured properly");

                _ = builder.UseSqlServer(realEstateAgentRankerRepositorySettings.ConnectionString);
            });
    }
}
