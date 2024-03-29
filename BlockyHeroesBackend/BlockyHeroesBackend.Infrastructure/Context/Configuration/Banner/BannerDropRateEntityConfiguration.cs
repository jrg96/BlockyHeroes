using BlockyHeroesBackend.Domain.Common.ValueObjects.Banner;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Common;
using BlockyHeroesBackend.Domain.Entities.Banner;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlockyHeroesBackend.Infrastructure.Context.Configuration.Banner;

public class BannerDropRateEntityConfiguration : IEntityTypeConfiguration<BannerDropRate>
{
    public void Configure(EntityTypeBuilder<BannerDropRate> builder)
    {
        builder
            .HasKey(dropRate => dropRate.Id);

        // Foreign key configurations
        builder
            .HasOne(dropRate => dropRate.GachaBanner)
            .WithMany(banner => banner.DropRates)
            .HasForeignKey(dropRate => dropRate.GachaBannerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // Conversions
        builder
            .Property(dropRate => dropRate.Id)
            .HasConversion(
                id => id.Id,
                value => new BannerDropRateId(value));

        builder
            .Property(dropRate => dropRate.GachaBannerId)
            .HasConversion(
                id => id.Id,
                value => new GachaBannerId(value));

        builder
            .Property(dropRate => dropRate.Rarity)
            .HasConversion(
                rarity => rarity.Id,
                value => ItemRarity.Get(value));
    }
}
