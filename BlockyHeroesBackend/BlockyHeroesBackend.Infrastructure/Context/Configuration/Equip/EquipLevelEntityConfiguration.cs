using BlockyHeroesBackend.Domain.Common.ValueObjects.Equip;
using BlockyHeroesBackend.Domain.Entities.Equip;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlockyHeroesBackend.Infrastructure.Context.Configuration.Equip;

public class EquipLevelEntityConfiguration : IEntityTypeConfiguration<EquipLevel>
{
    public void Configure(EntityTypeBuilder<EquipLevel> builder)
    {
        builder
            .HasKey(equip => equip.Id);

        // Foreign Key relationship
        builder
            .HasOne(equipLevel => equipLevel.Equip)
            .WithMany(equip => equip.EquipmentEvolutions)
            .HasForeignKey(equipLevel => equipLevel.EquipId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // Conversions
        builder
            .Property(equip => equip.Id)
            .HasConversion(
                id => id.Id,
                value => new EquipLevelId(value));
    }
}
