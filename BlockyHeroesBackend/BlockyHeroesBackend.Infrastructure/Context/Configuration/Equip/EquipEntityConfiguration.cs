using BlockyHeroesBackend.Domain.Common.ValueObjects.Equip;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlockyHeroesBackend.Infrastructure.Context.Configuration.Equip;

public class EquipEntityConfiguration : IEntityTypeConfiguration<Domain.Entities.Equip.Equip>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Equip.Equip> builder)
    {
        builder
            .HasKey(equip => equip.Id);

        // Conversion types
        builder
            .Property(equip => equip.Id)
            .HasConversion(
                id => id.Id,
                value => new EquipId(value));
    }
}
