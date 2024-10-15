using Brunda.Configuration.Common.Extensions;
using Brunda.External.PartnerApi.Contracts.Providers;
using Brunda.External.PartnerApi.Rest.Clients;
using Brunda.External.PartnerApi.Rest.Configuration;
using Brunda.External.PartnerApi.Rest.Constants;
using Brunda.External.PartnerApi.Rest.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;
using Refit;
using System.Threading.RateLimiting;

namespace Brunda.External.PartnerApi.Rest.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPartnerApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        var partnerApiConfiguration = configuration.GetRequiredSection(ConfigurationConstants.PartnerApiSectionKey);
        _ = services
            .AddOptionsWithValidation<PartnerApiSettings>(partnerApiConfiguration)
            .AddScoped<IRealEstateAgentProvider, RealEstateAgentProvider>();

        _ = services
            .AddResiliencePipeline(ResiliencePipelineConstants.PartnerApiKey, (builder, context) =>
            {
                var resilienceOptions = partnerApiConfiguration.GetRequiredSection(ConfigurationConstants.ResilienceOptionsSectionKey).Get<ResilienceOptionsSettings>()
                    ?? throw new InvalidOperationException($"{nameof(ResilienceOptionsSettings)} not configured properly");

                _ = builder
                    .AddRetry(new RetryStrategyOptions
                    {
                        BackoffType = DelayBackoffType.Exponential,
                        Delay = resilienceOptions.RetryOptions.Delay,
                        MaxRetryAttempts = resilienceOptions.RetryOptions.MaxRetryAttempts
                    })
                    .AddRateLimiter(new FixedWindowRateLimiter(
                        new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = resilienceOptions.RateLimitOptions.PermitLimit,
                            Window = resilienceOptions.RateLimitOptions.LimitWindow,
                            QueueLimit = 0,
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                        }));
            });

        _ = services
            .AddRefitClient<IPartnerApiClient>()
            .ConfigureHttpClient((provider, client) =>
            {
                var partnerApiSettings = partnerApiConfiguration.Get<PartnerApiSettings>()
                    ?? throw new InvalidOperationException($"{nameof(PartnerApiSettings)} not configured properly");

                client.BaseAddress = partnerApiSettings.BaseAddress;
            })
            .AddStandardResilienceHandler();

        return services;
    }
}
