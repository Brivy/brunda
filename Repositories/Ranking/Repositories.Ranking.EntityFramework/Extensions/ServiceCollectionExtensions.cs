using Funda.Assigment.Configuration.Common.Extensions;
using Funda.Assigment.Repositories.Ranking.Contracts.Repositories;
using Funda.Assigment.Repositories.Ranking.EntityFramework.Configuration;
using Funda.Assigment.Repositories.Ranking.EntityFramework.Constants;
using Funda.Assigment.Repositories.Ranking.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Funda.Assigment.Repositories.Ranking.EntityFramework.Extensions;

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
