using BlockyHeroesBackend.Domain.Common.ValueObjects.Character;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using BlockyHeroesBackend.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlockyHeroesBackend.Infrastructure.Context.Configuration.User;

public class UserCharacterEntityConfiguration : IEntityTypeConfiguration<UserCharacter>
{
    public void Configure(EntityTypeBuilder<UserCharacter> builder)
    {
        builder
            .HasKey(userChar => userChar.Id);

        // Foreign Key configurations
        builder
            .HasOne(userChar => userChar.Owner)
            .WithMany(user => user.UserCharacters)
            .HasForeignKey(userChar => userChar.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(userChar => userChar.CharacterLevel)
            .WithMany(charLevel => charLevel.UserCharacters)
            .HasForeignKey(userChar => userChar.CharacterLevelId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder // An Equipment can only be assigned to 1 char only
            .HasOne(userChar => userChar.UserEquipmentSlot1)
            .WithOne(userEquip => userEquip.UserCharacterSlot1)
            .HasForeignKey<UserCharacter>(userChar => userChar.UserEquipmentIdSlot1)
            .OnDelete(DeleteBehavior.NoAction); // On delete userequip, simply set null slot 1 or 2

        builder // An Equipment can only be assigned to 1 char only
            .HasOne(userChar => userChar.UserEquipmentSlot2)
            .WithOne(userEquip => userEquip.UserCharacterSlot2)
            .HasForeignKey<UserCharacter>(userChar => userChar.UserEquipmentIdSlot2)
            .OnDelete(DeleteBehavior.NoAction); // On delete userequip, simply set null slot 1 or 2


        // Conversions
        builder
            .Property(userChar => userChar.Id)
            .HasConversion(
                id => id.Id,
                value => new UserCharacterId(value));

        builder
            .Property(userChar => userChar.UserId)
            .HasConversion(
                id => id.Value,
                value => new UserId(value));

        builder
            .Property(userChar => userChar.CharacterLevelId)
            .HasConversion(
                id => id.Id,
                value => new CharacterLevelId(value));

        builder
            .Property(userChar => userChar.UserEquipmentIdSlot1)
            .HasConversion(
                id => id.Value.Value,
                value => new UserEquipmentId(value));

        builder
            .Property(userChar => userChar.UserEquipmentIdSlot2)
            .HasConversion(
                id => id.Value.Value,
                value => new UserEquipmentId(value));
    }
}
