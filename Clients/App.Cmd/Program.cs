using Brunda.Business.Ranking.Contracts;
using Brunda.Business.Ranking.Extensions;
using Brunda.External.PartnerApi.Rest.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
               .Build();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        _ = services
            .AddRankingServices(configuration)
            .AddPartnerApiServices(configuration);
    })
    .Build();

using (var serviceScope = host.Services.CreateScope())
{
    var serviceProvider = serviceScope.ServiceProvider;
    var realEstateRanker = serviceProvider.GetRequiredService<IRealEstateAgentRanker>();
    await realEstateRanker.RankAsync(CancellationToken.None).ConfigureAwait(false);
    await realEstateRanker.RankWithGardenAsync(CancellationToken.None).ConfigureAwait(false);
}

Console.ReadLine();
