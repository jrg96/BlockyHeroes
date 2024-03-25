using BlockyHeroesBackend.Domain.Common.ValueObjects.Item;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlockyHeroesBackend.Infrastructure.Context.Configuration.Item;

public class ItemEntityConfiguration : IEntityTypeConfiguration<Domain.Entities.Item.Item>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Item.Item> builder)
    {
        builder
            .HasKey(item => item.Id);

        builder
            .Property(item => item.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property (item => item.Description)
            .IsRequired()
            .HasMaxLength(600);

        // Conversions
        builder
            .Property(item => item.Id)
            .HasConversion(
                id => id.Id,
                value => new ItemId(value));

        builder
            .Property(item => item.Type)
            .HasConversion(
                type => type.Id,
                value => ItemTypes.Get(value));
    }
}
