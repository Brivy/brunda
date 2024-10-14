using Brunda.Modules.Ranking.Repositories.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brunda.Modules.Ranking.Repositories.EntityFramework.Configurations;

internal class ForSaleRankingConfiguration : IEntityTypeConfiguration<ForSaleRanking>
{
    public void Configure(EntityTypeBuilder<ForSaleRanking> builder)
    {
        _ = builder.ToTable("ForSaleRanking");

        _ = builder
            .HasKey(x => x.Id)
            .HasName("PK_ForSaleRanking");

        _ = builder
            .Property(x => x.RealEstateAgentName)
            .HasMaxLength(255);
    }
}
