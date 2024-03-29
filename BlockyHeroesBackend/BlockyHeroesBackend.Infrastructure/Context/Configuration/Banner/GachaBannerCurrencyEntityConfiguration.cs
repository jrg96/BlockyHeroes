using BlockyHeroesBackend.Domain.Common.ValueObjects.Banner;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Item;
using BlockyHeroesBackend.Domain.Entities.Banner;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlockyHeroesBackend.Infrastructure.Context.Configuration.Banner;

public class GachaBannerCurrencyEntityConfiguration : IEntityTypeConfiguration<GachaBannerCurrency>
{
    public void Configure(EntityTypeBuilder<GachaBannerCurrency> builder)
    {
        builder
            .HasKey(currency => currency.Id);

        // Foreign key configurations
        builder
            .HasOne(currency => currency.Item)
            .WithMany(item => item.GachaBannerCurrencies)
            .HasForeignKey(currency => currency.ItemId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(currency => currency.GachaBanner)
            .WithMany(banner => banner.GachaBannerCurrencies)
            .HasForeignKey(currency => currency.GachaBannerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // Conversions
        builder
            .Property(currency => currency.Id)
            .HasConversion(
                id => id.Id,
                value => new GachaBannerCurrencyId(value));

        builder
            .Property(currency => currency.ItemId)
            .HasConversion(
                id => id.Id,
                value => new ItemId(value));

        builder
            .Property(currency => currency.GachaBannerId)
            .HasConversion(
                id => id.Id,
                value => new GachaBannerId(value));
    }
}
