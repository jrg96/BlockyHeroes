using BlockyHeroesBackend.Domain.Common.ValueObjects.Character;
using BlockyHeroesBackend.Domain.Entities.Character;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlockyHeroesBackend.Infrastructure.Context.Configuration.Character;

public class CharacterLevelEntityConfiguration : IEntityTypeConfiguration<CharacterLevel>
{
    public void Configure(EntityTypeBuilder<CharacterLevel> builder)
    {
        builder
            .HasKey(charLevel => charLevel.Id);

        // Foreign Key configuration
        builder
            .HasOne(charLevel => charLevel.Character)
            .WithMany(character => character.CharacterLevels)
            .HasForeignKey(charLevel => charLevel.CharacterId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // Conversions
        builder
            .Property(charLevel => charLevel.Id)
            .HasConversion(
                id => id.Id,
                value => new CharacterLevelId(value));

        builder
            .Property(charLevel => charLevel.CharacterId)
            .HasConversion(
                id => id.Id,
                value => new CharacterId(value));
    }
}
