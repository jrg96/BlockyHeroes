using BlockyHeroesBackend.Domain.Common.ValueObjects.Character;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlockyHeroesBackend.Infrastructure.Context.Configuration.Character;

public class CharacterEntityConfiguration : IEntityTypeConfiguration<Domain.Entities.Character.Character>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Character.Character> builder)
    {
        builder
            .HasKey(character => character.Id);

        builder
            .Property(character => character.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(character => character.Rarity)
            .IsRequired();

        builder
            .Property (character => character.Description)
            .IsRequired()
            .HasMaxLength(500);

        // Conversions
        builder
            .Property(character => character.Id)
            .HasConversion(
                id => id.Id,
                value => new CharacterId(value));

        builder
            .Property(character => character.Rarity)
            .HasConversion(
                rarity => rarity.Id,
                value => ItemRarity.Get(value));
    }
}
