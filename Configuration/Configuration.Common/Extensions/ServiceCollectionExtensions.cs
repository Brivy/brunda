using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Brunda.Configuration.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOptionsWithValidation<TOptions>(this IServiceCollection services, IConfigurationSection configurationSection)
        where TOptions : class
    {
        return services
            .AddOptions<TOptions>()
            .Bind(configurationSection)
            .ValidateDataAnnotations()
            .ValidateOnStart()
            .Services;
    }
}
