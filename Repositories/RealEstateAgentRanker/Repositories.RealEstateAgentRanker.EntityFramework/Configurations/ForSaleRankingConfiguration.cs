using Funda.Assigment.Repositories.RealEstateAgentRanker.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Funda.Assigment.Repositories.RealEstateAgentRanker.EntityFramework.Configurations;

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
