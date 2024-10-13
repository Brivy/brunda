using Funda.Assigment.Repositories.Ranking.EntityFramework.Configurations;
using Funda.Assigment.Repositories.Ranking.EntityFramework.Constants;
using Funda.Assigment.Repositories.Ranking.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace Funda.Assigment.Repositories.Ranking.EntityFramework;

internal class RankingContext : DbContext
{
    public RankingContext() { }
    public RankingContext(DbContextOptions<RankingContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }

        _ = optionsBuilder.UseSqlServer("Server=.;Database=FundaAssigment;Trusted_Connection=True;TrustServerCertificate=True", options =>
        {
            _ = options.MigrationsHistoryTable("__RankingContextMigrations", DatabaseConstants.RankingSchemaName);
        });
    }

    public DbSet<ForSaleRanking> ForSaleRankings => Set<ForSaleRanking>();
    public DbSet<ForSaleWithGardenRanking> ForSaleWithGardenRankings => Set<ForSaleWithGardenRanking>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder
            .HasDefaultSchema(DatabaseConstants.RankingSchemaName)
            .ApplyConfiguration(new ForSaleRankingConfiguration())
            .ApplyConfiguration(new ForSaleWithGardenRankingConfiguration());
    }
}
