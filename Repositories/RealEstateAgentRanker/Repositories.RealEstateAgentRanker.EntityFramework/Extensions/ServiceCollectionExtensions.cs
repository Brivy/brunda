using Funda.Assigment.Configuration.Common.Extensions;
using Funda.Assigment.Repositories.RealEstateAgentRanker.EntityFramework.Configuration;
using Funda.Assigment.Repositories.RealEstateAgentRanker.EntityFramework.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Funda.Assigment.Repositories.RealEstateAgentRanker.EntityFramework.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRealEstateAgentRankerRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        var realEstateAgentRankerRepositoryConfiguration = configuration.GetRequiredSection(ConfigurationConstants.RankingRepositorySectionKey);
        return services
            .AddOptionsWithValidation<RankingRepositorySettings>(realEstateAgentRankerRepositoryConfiguration)
            .AddDbContext<RankingContext>(builder =>
            {
                var realEstateAgentRankerRepositorySettings = realEstateAgentRankerRepositoryConfiguration.Get<RankingRepositorySettings>()
                    ?? throw new InvalidOperationException($"{nameof(RankingRepositorySettings)} not configured properly");

                _ = builder.UseSqlServer(realEstateAgentRankerRepositorySettings.ConnectionString);
            });
    }
}
