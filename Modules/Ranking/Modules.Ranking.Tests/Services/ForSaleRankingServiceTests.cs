using Brunda.Modules.Ranking.Repositories.Contracts.Models;
using Brunda.Modules.Ranking.Repositories.Contracts.Repositories;
using Brunda.Modules.Ranking.Services;
using Microsoft.Extensions.Logging;
using Modules.Ranking.Repositories.Tests.Builder.Models;
using Moq;

namespace Brunda.Modules.Ranking.Tests.Services;

public class ForSaleRankingServiceTests
{
    [Fact]
    public async Task When_RankAsync_Then_Rank()
    {
        // Arrange
        var forSaleRankingRepositoryMock = new Mock<IForSaleRankingRepository>();
        var loggerMock = new Mock<ILogger<ForSaleRankingService>>();

        var cancellationToken = new CancellationTokenSource().Token;
        var forSaleRanking = new ForSaleRankingModelBuilder().Build();
        var forSaleRankingCollection = new List<ForSaleRankingModel> { forSaleRanking };

        var sut = new ForSaleRankingService(forSaleRankingRepositoryMock.Object, loggerMock.Object);

        // Act
        await sut.RankAsync(forSaleRankingCollection, cancellationToken);

        // Assert
        forSaleRankingRepositoryMock.Verify(x => x.ClearRankingAsync(cancellationToken), Times.Once);
        forSaleRankingRepositoryMock.Verify(x => x.CreateRankingAsync(forSaleRankingCollection, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Given_Exception_When_RankAsync_Then_CancelRanking()
    {
        // Arrange
        var forSaleRankingRepositoryMock = new Mock<IForSaleRankingRepository>();
        var loggerMock = new Mock<ILogger<ForSaleRankingService>>();

        var cancellationToken = new CancellationTokenSource().Token;
        var forSaleRanking = new ForSaleRankingModelBuilder().Build();
        var forSaleRankingCollection = new List<ForSaleRankingModel> { forSaleRanking };

        forSaleRankingRepositoryMock.Setup(x => x.ClearRankingAsync(cancellationToken))
            .ThrowsAsync(new Exception()).Verifiable();

        var sut = new ForSaleRankingService(forSaleRankingRepositoryMock.Object, loggerMock.Object);

        // Act
        await sut.RankAsync(forSaleRankingCollection, cancellationToken);

        // Assert
        forSaleRankingRepositoryMock.Verify();
        forSaleRankingRepositoryMock.Verify(x => x.CreateRankingAsync(forSaleRankingCollection, cancellationToken), Times.Never);
    }
}
