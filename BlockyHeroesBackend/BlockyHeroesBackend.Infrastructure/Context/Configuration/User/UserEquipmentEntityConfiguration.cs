using BlockyHeroesBackend.Domain.Common.ValueObjects.Equip;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using BlockyHeroesBackend.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlockyHeroesBackend.Infrastructure.Context.Configuration.User;

public class UserEquipmentEntityConfiguration : IEntityTypeConfiguration<UserEquipment>
{
    public void Configure(EntityTypeBuilder<UserEquipment> builder)
    {
        builder
            .HasKey(userEquip => userEquip.Id);

        // Foreign Key
        // User to EquipLevel
        builder
            .HasOne(userEquip => userEquip.Owner)
            .WithMany(user => user.UserEquipment)
            .HasForeignKey(userEquip => userEquip.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // EquipLevel to User (owner)
        builder
            .HasOne(userEquip => userEquip.EquipLevel)
            .WithMany(equipLevel => equipLevel.UserEquipment)
            .HasForeignKey(userEquip => userEquip.EquipLevelId)
            .OnDelete(DeleteBehavior.Cascade);

        // Conversions
        builder
            .Property(userEquip => userEquip.Id)
            .HasConversion(
                id => id.Value,
                value => new UserEquipmentId(value));

        builder
            .Property(userEquip => userEquip.UserId)
            .HasConversion(
                userId => userId.Value,
                value => new UserId(value));

        builder
            .Property(userEquip => userEquip.EquipLevelId)
            .HasConversion(
                equipLevelId => equipLevelId.Id,
                value => new EquipLevelId(value)) ;
    }
}
