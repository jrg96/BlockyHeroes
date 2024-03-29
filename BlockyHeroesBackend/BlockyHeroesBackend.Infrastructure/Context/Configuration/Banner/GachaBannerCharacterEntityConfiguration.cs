using BlockyHeroesBackend.Domain.Common.ValueObjects.Banner;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Character;
using BlockyHeroesBackend.Domain.Entities.Banner;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlockyHeroesBackend.Infrastructure.Context.Configuration.Banner;

public class GachaBannerCharacterEntityConfiguration : IEntityTypeConfiguration<GachaBannerCharacter>
{
    public void Configure(EntityTypeBuilder<GachaBannerCharacter> builder)
    {
        builder
            .HasKey(gachaCharacter => gachaCharacter.Id);

        // Conversions
        builder
            .Property(gachaCharacter => gachaCharacter.Id)
            .HasConversion(
                id => id.Id,
                value => new GachaBannerCharacterId(value));

        builder
            .Property(gachaCharacter => gachaCharacter.GachaBannerId)
            .HasConversion(
                id => id.Id,
                value => new GachaBannerId(value));

        builder
            .Property(gachaCharacter => gachaCharacter.CharacterLevelId)
            .HasConversion(
                id => id.Id,
                value => new CharacterLevelId(value));
    }
}
