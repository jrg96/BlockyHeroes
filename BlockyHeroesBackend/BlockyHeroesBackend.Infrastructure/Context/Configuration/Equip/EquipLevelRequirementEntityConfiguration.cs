using BlockyHeroesBackend.Domain.Common.ValueObjects.Equip;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Item;
using BlockyHeroesBackend.Domain.Entities.Equip;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlockyHeroesBackend.Infrastructure.Context.Configuration.Equip;

public class EquipLevelRequirementEntityConfiguration : IEntityTypeConfiguration<EquipLevelRequirement>
{
    public void Configure(EntityTypeBuilder<EquipLevelRequirement> builder)
    {
        builder
            .HasKey(requirement => requirement.Id);

        // Foreign Key configurations
        builder
            .HasOne(requirement => requirement.EquipLevel)
            .WithMany(equipLevel => equipLevel.EquipLevelRequirements)
            .HasForeignKey(requirement => requirement.EquipLevelId);

        builder
            .HasOne(requirement => requirement.Item)
            .WithMany(item => item.EquipLevelRequirements)
            .HasForeignKey(requirement => requirement.ItemId);

        // Custom Conversions
        builder
            .Property(requirement => requirement.Id)
            .HasConversion(
                id => id.Id,
                value => new EquipLevelRequirementId(value));

        builder
            .Property(requirement => requirement.EquipLevelId)
            .HasConversion(
                id => id.Id,
                value => new EquipLevelId(value));

        builder
            .Property(requirement => requirement.ItemId)
            .HasConversion(
                id => id.Id,
                value => new ItemId(value));
    }
}
