using Funda.Assigment.Repositories.RealEstateAgentRanker.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Funda.Assigment.Repositories.RealEstateAgentRanker.EntityFramework.Configurations;

internal class ForSaleWithGardenRankingConfiguration : IEntityTypeConfiguration<ForSaleWithGardenRanking>
{
    public void Configure(EntityTypeBuilder<ForSaleWithGardenRanking> builder)
    {
        _ = builder.ToTable("ForSaleWithGardenRanking");

        _ = builder
            .HasKey(x => x.Id)
            .HasName("PK_ForSaleWithGardenRanking");

        _ = builder
            .Property(x => x.RealEstateAgentName)
            .HasMaxLength(255);
    }
}

