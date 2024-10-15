using Brunda.Modules.Ranking.Repositories.Contracts.Models;
using Brunda.Modules.Ranking.Repositories.Contracts.Repositories;
using Brunda.Modules.Ranking.Services;
using Microsoft.Extensions.Logging;
using Modules.Ranking.Repositories.Tests.Builder.Models;
using Moq;

namespace Brunda.Modules.Ranking.Tests.Services;

public class ForSaleWithGardenRankingServiceTests
{
    [Fact]
    public async Task When_RankAsync_Then_Rank()
    {
        // Arrange
        var forSaleWithGardenRankingRepositoryMock = new Mock<IForSaleWithGardenRankingRepository>();
        var loggerMock = new Mock<ILogger<ForSaleWithGardenRankingService>>();

        var cancellationToken = new CancellationTokenSource().Token;
        var forSaleWithGardenRanking = new ForSaleWithGardenRankingModelBuilder().Build();
        var forSaleWithGardenRankingCollection = new List<ForSaleWithGardenRankingModel> { forSaleWithGardenRanking };

        var sut = new ForSaleWithGardenRankingService(forSaleWithGardenRankingRepositoryMock.Object, loggerMock.Object);

        // Act
        await sut.RankAsync(forSaleWithGardenRankingCollection, cancellationToken);

        // Assert
        forSaleWithGardenRankingRepositoryMock.Verify(x => x.ClearRankingAsync(cancellationToken), Times.Once);
        forSaleWithGardenRankingRepositoryMock.Verify(x => x.CreateRankingAsync(forSaleWithGardenRankingCollection, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Given_Exception_When_RankAsync_Then_CancelRanking()
    {
        // Arrange
        var IForSaleWithGardenRankingRepository = new Mock<IForSaleWithGardenRankingRepository>();
        var loggerMock = new Mock<ILogger<ForSaleWithGardenRankingService>>();

        var cancellationToken = new CancellationTokenSource().Token;
        var forSaleWithGardenRanking = new ForSaleWithGardenRankingModelBuilder().Build();
        var forSaleWithGardenRankingCollection = new List<ForSaleWithGardenRankingModel> { forSaleWithGardenRanking };

        IForSaleWithGardenRankingRepository.Setup(x => x.ClearRankingAsync(cancellationToken))
            .ThrowsAsync(new Exception()).Verifiable();

        var sut = new ForSaleWithGardenRankingService(IForSaleWithGardenRankingRepository.Object, loggerMock.Object);

        // Act
        await sut.RankAsync(forSaleWithGardenRankingCollection, cancellationToken);

        // Assert
        IForSaleWithGardenRankingRepository.Verify();
        IForSaleWithGardenRankingRepository.Verify(x => x.CreateRankingAsync(forSaleWithGardenRankingCollection, cancellationToken), Times.Never);
    }
}
