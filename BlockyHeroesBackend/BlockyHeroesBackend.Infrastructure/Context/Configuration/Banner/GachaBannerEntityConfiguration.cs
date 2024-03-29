using BlockyHeroesBackend.Domain.Common.ValueObjects.Banner;
using BlockyHeroesBackend.Domain.Entities.Banner;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlockyHeroesBackend.Infrastructure.Context.Configuration.Banner;

public class GachaBannerEntityConfiguration : IEntityTypeConfiguration<GachaBanner>
{
    public void Configure(EntityTypeBuilder<GachaBanner> builder)
    {
        builder
            .HasKey(banner => banner.Id);

        builder
            .Property(banner => banner.Name)
            .IsRequired()
            .HasMaxLength(120);

        builder
            .Property(banner => banner.StartDate)
            .IsRequired();

        builder
            .Property(banner => banner.EndDate)
            .IsRequired();

        // Conversions
        builder
            .Property(banner => banner.Id)
            .HasConversion(
                id => id.Id,
                value => new GachaBannerId(value));

        builder
            .Property(banner => banner.Type)
            .HasConversion(
                type => type.Id,
                value => BannerType.Get(value));
    }
}
