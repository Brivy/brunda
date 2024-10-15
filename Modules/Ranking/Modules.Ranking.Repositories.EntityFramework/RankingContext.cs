using Brunda.Modules.Ranking.Repositories.EntityFramework.Configurations;
using Brunda.Modules.Ranking.Repositories.EntityFramework.Constants;
using Brunda.Modules.Ranking.Repositories.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace Brunda.Modules.Ranking.Repositories.EntityFramework;

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

        _ = optionsBuilder.UseSqlServer("Server=.;Database=Brunda;Trusted_Connection=True;TrustServerCertificate=True", options =>
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
