using Brunda.External.PartnerApi.Tests.Builder.Models;
using Brunda.Modules.Ranking.Configuration;
using Brunda.Modules.Ranking.Contracts;
using Brunda.Modules.Ranking.Repositories.Contracts.Models;
using Brunda.Modules.Ranking.Tests.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Brunda.Modules.Ranking.Tests;

public partial class RealEstateAgentRankerTests
{
    [Fact]
    public async Task Given_NoSummaries_When_RankWithGardenAsync_Then_DoNothing()
    {
        // Arrange
        var forSaleRankingServiceMock = new Mock<IForSaleRankingService>();
        var forSaleWithGardenRankingServiceMock = new Mock<IForSaleWithGardenRankingService>();
        var realEstateAgentServiceMock = new Mock<IRealEstateAgentService>();
        var optionsMock = new Mock<IOptions<RealEstateAgentRankerSettings>>();
        var loggerMock = new Mock<ILogger<RealEstateAgentRanker>>();

        var cancellationToken = new CancellationTokenSource().Token;
        var settings = new RealEstateAgentRankerSettingsBuilder().Build();

        optionsMock.SetupGet(x => x.Value)
            .Returns(settings).Verifiable();

        realEstateAgentServiceMock.Setup(x => x.GetSummariesAsync(It.IsAny<string>(), true, cancellationToken))
            .ReturnsAsync([]).Verifiable();

        var sut = new RealEstateAgentRanker(forSaleRankingServiceMock.Object, forSaleWithGardenRankingServiceMock.Object,
            realEstateAgentServiceMock.Object, optionsMock.Object, loggerMock.Object);

        // Act
        await sut.RankWithGardenAsync(cancellationToken);

        // Assert
        optionsMock.Verify();
        realEstateAgentServiceMock.Verify();
        forSaleWithGardenRankingServiceMock.Verify(x => x.RankAsync(It.IsAny<List<ForSaleWithGardenRankingModel>>(), cancellationToken), Times.Never);
    }

    [Fact]
    public async Task Given_OneSummary_When_RankWithGardenAsync_Then_RankTheRealEstateAgent()
    {
        // Arrange
        const int forSaleCount = 42;
        const string realEstateAgentName = "The Real Estate Agent";
        const string searchLocation = "amstordam";

        var forSaleRankingServiceMock = new Mock<IForSaleRankingService>();
        var forSaleWithGardenRankingServiceMock = new Mock<IForSaleWithGardenRankingService>();
        var realEstateAgentServiceMock = new Mock<IRealEstateAgentService>();
        var optionsMock = new Mock<IOptions<RealEstateAgentRankerSettings>>();
        var loggerMock = new Mock<ILogger<RealEstateAgentRanker>>();

        var cancellationToken = new CancellationTokenSource().Token;
        var settings = new RealEstateAgentRankerSettingsBuilder()
            .WithSearchLocation(searchLocation)
            .Build();
        var realEstateAgentSummary = new RealEstateAgentSummaryModelBuilder()
            .WithForSaleCount(forSaleCount)
            .WithRealEstateAgentName(realEstateAgentName)
            .Build();

        optionsMock.SetupGet(x => x.Value)
            .Returns(settings).Verifiable();

        realEstateAgentServiceMock.Setup(x => x.GetSummariesAsync(searchLocation, true, cancellationToken))
            .ReturnsAsync([realEstateAgentSummary]).Verifiable();

        var sut = new RealEstateAgentRanker(forSaleRankingServiceMock.Object, forSaleWithGardenRankingServiceMock.Object,
            realEstateAgentServiceMock.Object, optionsMock.Object, loggerMock.Object);

        // Act
        await sut.RankWithGardenAsync(cancellationToken);

        // Assert
        optionsMock.Verify();
        realEstateAgentServiceMock.Verify();
        forSaleWithGardenRankingServiceMock.Verify(x => x.RankAsync(It.Is<List<ForSaleWithGardenRankingModel>>(x =>
            x[0].Name == realEstateAgentName &&
            x[0].ForSaleCount == forSaleCount), cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Given_MultipleSummaries_When_RankWithGardenAsync_Then_RankTheRealEstateAgents()
    {
        // Arrange
        const int primaryForSaleCount = 42;
        const int secondaryForSaleCount = 24;
        const string primaryRealEstateAgentName = "The Real Estate Agent";
        const string secondaryRealEstateAgentName = "The Realest Estate Agent";

        var forSaleRankingServiceMock = new Mock<IForSaleRankingService>();
        var forSaleWithGardenRankingServiceMock = new Mock<IForSaleWithGardenRankingService>();
        var realEstateAgentServiceMock = new Mock<IRealEstateAgentService>();
        var optionsMock = new Mock<IOptions<RealEstateAgentRankerSettings>>();
        var loggerMock = new Mock<ILogger<RealEstateAgentRanker>>();

        var cancellationToken = new CancellationTokenSource().Token;
        var settings = new RealEstateAgentRankerSettingsBuilder().Build();
        var primaryRealEstateAgentSummary = new RealEstateAgentSummaryModelBuilder()
            .WithForSaleCount(primaryForSaleCount)
            .WithRealEstateAgentName(primaryRealEstateAgentName)
            .Build();
        var secondaryRealEstateAgentSummary = new RealEstateAgentSummaryModelBuilder()
            .WithForSaleCount(secondaryForSaleCount)
            .WithRealEstateAgentName(secondaryRealEstateAgentName)
            .Build();

        optionsMock.SetupGet(x => x.Value)
            .Returns(settings).Verifiable();

        realEstateAgentServiceMock.Setup(x => x.GetSummariesAsync(It.IsAny<string>(), true, cancellationToken))
            .ReturnsAsync([primaryRealEstateAgentSummary, secondaryRealEstateAgentSummary]).Verifiable();

        var sut = new RealEstateAgentRanker(forSaleRankingServiceMock.Object, forSaleWithGardenRankingServiceMock.Object,
            realEstateAgentServiceMock.Object, optionsMock.Object, loggerMock.Object);

        // Act
        await sut.RankWithGardenAsync(cancellationToken);

        // Assert
        optionsMock.Verify();
        realEstateAgentServiceMock.Verify();
        forSaleWithGardenRankingServiceMock.Verify(x => x.RankAsync(It.Is<List<ForSaleWithGardenRankingModel>>(x =>
            x[0].Name == primaryRealEstateAgentName &&
            x[0].ForSaleCount == primaryForSaleCount &&
            x[1].Name == secondaryRealEstateAgentName &&
            x[1].ForSaleCount == secondaryForSaleCount), cancellationToken), Times.Once);
    }
}
