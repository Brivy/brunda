# Brunda

Small console application that combines the power of me (Brian) and Funda to make "Brunda".

## What does it do?

This application will create a top 10 of all real estate agents in a certain region.
It will do this twice for the same area; one for all available options and one time for all available options with a garden.
Certain regions will take longer to query because of their size (like 'Amsterdam').

## How does it work?

Simply clone the application and supply the following in the `appsettings.json` (or `appsettings.Development.json`):

- `PartnerApi:ApiKey`. This value must be supplied with a Funda API key (only accessible by asking Funda very nicely).
- `Ranking:RealEstateAgentRanker:SearchLocation`. This is default set to Amsterdam, because it provides the biggest challenge.
- `Ranking:RankingRepository:ConnectionString`. Provide a connection string of your own SQL Server instance.

When all set and done, you'll only need the database tables inside your SQL Server instance.
To do this, navigate to `...\brunda\Modules\Ranking\Modules.Ranking.Repositories.EntityFramework` and execute the following command:

```bash
$ dotnet ef database update

Build started...
Build succeeded.
Applying migration '20241015152922_InitialCreate'.
Done.
```

Voila, you now have one database and three tables that are used by Brunda.

## How do you handle that PartnerApi

To prevent connection issues with the PartnerApi (or just straight up rate-limiting me by sending 401's), I've created a resilient environment.
This comes in two parts:

- Using the `.AddStandardResilienceHandler` of .NET 8. One extension method from `Polly` that is just the best for handling simple resiliency in API's (see [here](https://devblogs.microsoft.com/dotnet/building-resilient-cloud-services-with-dotnet-8/) for more information). This will **not** be enough to prevent the rate limiter from the PartnerApi.
- Creating my own `ResiliencePipelineBuilder` for the rate limiter.

A special case needs a special implementation. According to my documentation, the PartnerApi rate limits if you send more than 100 requests per minute. By building a pipeline that only let's 100 request pass per minute, we ensure that our code doesn't get an _unauthorized_ back (don't ask me why it's a 401 and not a 429):

```csharp
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
                    PermitLimit = resilienceOptions.RateLimitOptions.PermitLimit,
                    Window = resilienceOptions.RateLimitOptions.LimitWindow,
                    QueueLimit = 0,
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                }));
    });
```

## That architecture looks over-engineered

Yeah, probably could have done this in one library, but that's not the point.
I wanted to demonstrate my capabilities of creating an architecture that is very modulair and easy to extend (and I wanted to build something I was also proud of).

For now, the solution (in .NET terms) has four folders:

- Clients: Contains all front-facing logic for booting the application. This can be a setup for API stuff, but I did it for a console application.
- Configuration: Contains all _shared_ logic for configuration (and are not bound to any specific module). In particular, we have one extension method called `AddOptionsWithValidation` that validates and binds our settings to strongly-typed objects.
- External: Contains all external services that are not bound to any module. For us, this is the PartnerApi, because I can image having multiple modules working together with the same PartnerApi (this project is about Funda after all).
- Modules: Are basically logical groupings of related domain concepts (some DDD stuff, but that's not my strong suit). For example, I put ranking the real estate agents under the module `Ranking`. Here, all logic related to the ranking can be found, from business logic to a repository project. All while trying to make each piece as small and responsible for a specific set of logic.

This architecture also heavily relies on using the `public` vs `internal` keywords on classes. This way, every class can only interact with classes that are within it's domain. A cool demonstration of this is as follows:

- While `Clients.Cmd` has a relation with `Modules.Ranking` (which in terms has a relation with `Modules.Ranking.Contracts`), it can only access the `ServiceCollectionExtensions` extension method class for setting up the service collection. All the other classes and records within the project have the `internal` keyword.
- Because `Modules.Ranking` has the relationship with `Modules.Ranking.Contracts`, `Clients.Cmd` can interact with the public contracts that are exposed. This way, `Clients.Cmd` can still interact with the business logic within the `Modules.Ranking` without knowing the implementation, hurray!.

There are a ton more examples of this in the project, please take a look for yourself or contact me for the full explanation.

## Where are all the cool stuff

This application is more focused on getting the architecture right, then having for example a fully tested application with CI/CD that also has logging to files.
The best part about this application is that all of this is still possible because of the modulair nature of the architecture.
For now, only the `Modules.Ranking` is fully tested (for the sake of demonstration), but the rest will come maybe later...
