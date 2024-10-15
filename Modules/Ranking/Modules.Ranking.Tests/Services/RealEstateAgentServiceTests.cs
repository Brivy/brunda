using Brunda.External.PartnerApi.Contracts.Enums;
using Brunda.External.PartnerApi.Contracts.Models;
using Brunda.External.PartnerApi.Contracts.Options;
using Brunda.External.PartnerApi.Contracts.Providers;
using Brunda.External.PartnerApi.Tests.Builder.Models;
using Brunda.Modules.Ranking.Services;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Extensions.Logging;
using Moq;

namespace Brunda.Modules.Ranking.Tests.Services;

public class RealEstateAgentServiceTests
{
    [Fact]
    public async Task Given_OneResultFromApi_When_GetSummariesAsync_Then_ReturnSummaryCollection()
    {
        // Arrange
        const string location = "amstormdam";
        const int forSaleCount = 42;
        const int realEstateAgentId = 123;
        const string realEstateAgentName = "The realest";

        var realEstateAgentProviderMock = new Mock<IRealEstateAgentProvider>();
        var loggerMock = new Mock<ILogger<RealEstateAgentService>>();

        var cancellationToken = new CancellationTokenSource().Token;
        var realEstateAgentSummary = new RealEstateAgentSummaryModelBuilder()
            .WithForSaleCount(forSaleCount)
            .WithRealEstateAgentId(realEstateAgentId)
            .WithRealEstateAgentName(realEstateAgentName)
            .Build();
        var pageModel = new PageModelBuilder<RealEstateAgentSummaryModel>()
            .WithTotalPages(1)
            .WithCurrentPage(1)
            .WithResults([realEstateAgentSummary])
            .Build();

        realEstateAgentProviderMock.Setup(x => x.GetSummaryDataAsync(It.IsAny<OfferOptions>(), cancellationToken))
            .ReturnsAsync(pageModel).Verifiable();

        var sut = new RealEstateAgentService(realEstateAgentProviderMock.Object, loggerMock.Object);

        // Act
        var result = await sut.GetSummariesAsync(location, cancellationToken);

        // Assert
        using (new AssertionScope())
        {
            result.Should().NotBeEmpty().And.ContainSingle();

            var resultList = result.ToList();
            resultList[0].ForSaleCount.Should().Be(forSaleCount);
            resultList[0].RealEstateAgentId.Should().Be(realEstateAgentId);
            resultList[0].RealEstateAgentName.Should().Be(realEstateAgentName);
        }

        realEstateAgentProviderMock.Verify(x => x.GetSummaryDataAsync(It.Is<OfferOptions>(x =>
            x.Location == location &&
            !x.HasGarden &&
            x.Page == 1 &&
            x.PageSize == 25 &&
            x.ResidenceContractType == ResidenceContractType.Buy), cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Given_MultipleButTheSameResultsFromApi_When_GetSummariesAsync_Then_ReturnSummaryCollection()
    {
        // Arrange
        const int forSaleCount = 42;
        const int secondForSaleCount = 58;
        const int realEstateAgentId = 123;
        const string realEstateAgentName = "The realest";

        var realEstateAgentProviderMock = new Mock<IRealEstateAgentProvider>();
        var loggerMock = new Mock<ILogger<RealEstateAgentService>>();

        var cancellationToken = new CancellationTokenSource().Token;
        var realEstateAgentSummary = new RealEstateAgentSummaryModelBuilder()
            .WithForSaleCount(forSaleCount)
            .WithRealEstateAgentId(realEstateAgentId)
            .WithRealEstateAgentName(realEstateAgentName)
            .Build();
        var secondRealEstateAgentSummary = new RealEstateAgentSummaryModelBuilder()
            .WithForSaleCount(secondForSaleCount)
            .WithRealEstateAgentId(realEstateAgentId)
            .WithRealEstateAgentName(realEstateAgentName)
            .Build();
        var pageModel = new PageModelBuilder<RealEstateAgentSummaryModel>()
            .WithTotalPages(2)
            .WithCurrentPage(1)
            .WithResults([realEstateAgentSummary])
            .Build();
        var secondPageModel = new PageModelBuilder<RealEstateAgentSummaryModel>()
            .WithTotalPages(2)
            .WithCurrentPage(2)
            .WithResults([secondRealEstateAgentSummary])
            .Build();

        realEstateAgentProviderMock.SetupSequence(x => x.GetSummaryDataAsync(It.IsAny<OfferOptions>(), cancellationToken))
            .ReturnsAsync(pageModel)
            .ReturnsAsync(secondPageModel);

        var sut = new RealEstateAgentService(realEstateAgentProviderMock.Object, loggerMock.Object);

        // Act
        var result = await sut.GetSummariesAsync("amstormdam", cancellationToken);

        // Assert
        using (new AssertionScope())
        {
            result.Should().NotBeEmpty().And.ContainSingle();

            var resultList = result.ToList();
            resultList[0].ForSaleCount.Should().Be(forSaleCount + secondForSaleCount);
            resultList[0].RealEstateAgentId.Should().Be(realEstateAgentId);
            resultList[0].RealEstateAgentName.Should().Be(realEstateAgentName);
        }

        realEstateAgentProviderMock.Verify(x => x.GetSummaryDataAsync(It.IsAny<OfferOptions>(), cancellationToken), Times.Exactly(2));
    }

    [Fact]
    public async Task Given_MultipleOtherResultsFromApi_When_GetSummariesAsync_Then_ReturnSummaryCollection()
    {
        // Arrange
        const int primaryForSaleCount = 42;
        const int primaryRealEstateAgentId = 123;
        const string primaryRealEstateAgentName = "The realest";
        const int secondaryForSaleCount = 24;
        const int secondaryRealEstateAgentId = 321;
        const string secondaryRealEstateAgentName = "The other real one";

        var realEstateAgentProviderMock = new Mock<IRealEstateAgentProvider>();
        var loggerMock = new Mock<ILogger<RealEstateAgentService>>();

        var cancellationToken = new CancellationTokenSource().Token;
        var primaryRealEstateAgentSummary = new RealEstateAgentSummaryModelBuilder()
            .WithForSaleCount(primaryForSaleCount)
            .WithRealEstateAgentId(primaryRealEstateAgentId)
            .WithRealEstateAgentName(primaryRealEstateAgentName)
            .Build();
        var secondaryRealEstateAgentSummary = new RealEstateAgentSummaryModelBuilder()
            .WithForSaleCount(secondaryForSaleCount)
            .WithRealEstateAgentId(secondaryRealEstateAgentId)
            .WithRealEstateAgentName(secondaryRealEstateAgentName)
            .Build();
        var primaryPageModel = new PageModelBuilder<RealEstateAgentSummaryModel>()
            .WithTotalPages(2)
            .WithCurrentPage(1)
            .WithResults([primaryRealEstateAgentSummary])
            .Build();
        var secondaryPageModel = new PageModelBuilder<RealEstateAgentSummaryModel>()
            .WithTotalPages(2)
            .WithCurrentPage(2)
            .WithResults([secondaryRealEstateAgentSummary])
            .Build();

        realEstateAgentProviderMock.SetupSequence(x => x.GetSummaryDataAsync(It.IsAny<OfferOptions>(), cancellationToken))
            .ReturnsAsync(primaryPageModel)
            .ReturnsAsync(secondaryPageModel);

        var sut = new RealEstateAgentService(realEstateAgentProviderMock.Object, loggerMock.Object);

        // Act
        var result = await sut.GetSummariesAsync("amstormdam", cancellationToken);

        // Assert
        using (new AssertionScope())
        {
            result.Should().NotBeEmpty().And.HaveCount(2);

            var resultList = result.ToList();
            resultList[0].ForSaleCount.Should().Be(primaryForSaleCount);
            resultList[0].RealEstateAgentId.Should().Be(primaryRealEstateAgentId);
            resultList[0].RealEstateAgentName.Should().Be(primaryRealEstateAgentName);
            resultList[1].ForSaleCount.Should().Be(secondaryForSaleCount);
            resultList[1].RealEstateAgentId.Should().Be(secondaryRealEstateAgentId);
            resultList[1].RealEstateAgentName.Should().Be(secondaryRealEstateAgentName);
        }

        realEstateAgentProviderMock.Verify(x => x.GetSummaryDataAsync(It.IsAny<OfferOptions>(), cancellationToken), Times.Exactly(2));
    }

    [Fact]
    public async Task Given_ApiFailure_When_GetSummariesAsync_Then_ReturnCurrentResult()
    {
        // Arrange
        const int forSaleCount = 42;
        const int realEstateAgentId = 123;
        const string realEstateAgentName = "The realest";

        var realEstateAgentProviderMock = new Mock<IRealEstateAgentProvider>();
        var loggerMock = new Mock<ILogger<RealEstateAgentService>>();

        var cancellationToken = new CancellationTokenSource().Token;
        var realEstateAgentSummary = new RealEstateAgentSummaryModelBuilder()
            .WithForSaleCount(forSaleCount)
            .WithRealEstateAgentId(realEstateAgentId)
            .WithRealEstateAgentName(realEstateAgentName)
            .Build();
        var pageModel = new PageModelBuilder<RealEstateAgentSummaryModel>()
            .WithTotalPages(2)
            .WithCurrentPage(1)
            .WithResults([realEstateAgentSummary])
            .Build();

        realEstateAgentProviderMock.SetupSequence(x => x.GetSummaryDataAsync(It.IsAny<OfferOptions>(), cancellationToken))
            .ReturnsAsync(pageModel)
            .ReturnsAsync((PageModel<RealEstateAgentSummaryModel>?)null);

        var sut = new RealEstateAgentService(realEstateAgentProviderMock.Object, loggerMock.Object);

        // Act
        var result = await sut.GetSummariesAsync("amstormdam", cancellationToken);

        // Assert
        using (new AssertionScope())
        {
            result.Should().NotBeEmpty().And.ContainSingle();

            var resultList = result.ToList();
            resultList[0].ForSaleCount.Should().Be(forSaleCount);
            resultList[0].RealEstateAgentId.Should().Be(realEstateAgentId);
            resultList[0].RealEstateAgentName.Should().Be(realEstateAgentName);
        }

        realEstateAgentProviderMock.Verify(x => x.GetSummaryDataAsync(It.IsAny<OfferOptions>(), cancellationToken), Times.Exactly(2));
    }

}
