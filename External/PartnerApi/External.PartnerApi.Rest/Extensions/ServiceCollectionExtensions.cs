using Funda.Assigment.Configuration.Common.Extensions;
using Funda.Assigment.External.PartnerApi.Rest.Clients;
using Funda.Assigment.External.PartnerApi.Rest.Configuration;
using Funda.Assigment.External.PartnerApi.Rest.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;
using Refit;
using System.Threading.RateLimiting;

namespace Funda.Assigment.External.PartnerApi.Rest.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPartnerApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        var partnerApiConfiguration = configuration.GetRequiredSection(ConfigurationConstants.PartnerApiSectionKey);
        _ = services
            .AddOptionsWithValidation<PartnerApiSettings>(partnerApiConfiguration);

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
                        MaxRetryAttempts = resilienceOptions.RetryOptions.MaxRetryAttempts,
                        UseJitter = true
                    })
                    .AddRateLimiter(new FixedWindowRateLimiter(
                        new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = resilienceOptions.RateLimitOptions.PermitLimt,
                            Window = resilienceOptions.RateLimitOptions.LimtiWindow,
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
