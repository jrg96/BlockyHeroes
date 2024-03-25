using BlockyHeroesBackend.Domain.Common.ValueObjects.Character;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Item;
using BlockyHeroesBackend.Domain.Entities.Character;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlockyHeroesBackend.Infrastructure.Context.Configuration.Character;

public class CharacterLevelRequirementEntityConfiguration : IEntityTypeConfiguration<CharacterLevelRequirement>
{
    public void Configure(EntityTypeBuilder<CharacterLevelRequirement> builder)
    {
        builder
            .HasKey(charRequirement => charRequirement.Id);

        // Foreign key configurations
        builder
            .HasOne(charRequirement => charRequirement.CharacterLevel)
            .WithMany(charLevel => charLevel.CharacterLevelRequirements)
            .HasForeignKey(charRequirement => charRequirement.CharacterLevelId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(charRequirement => charRequirement.Item)
            .WithMany(item => item.CharacterLevelRequirements)
            .HasForeignKey(charRequirement => charRequirement.ItemId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);


        // Conversions
        builder
            .Property(charRequirement => charRequirement.Id)
            .HasConversion(
                id => id.Id,
                value => new CharacterLevelRequirementId(value));

        builder
            .Property(charRequirement => charRequirement.CharacterLevelId)
            .HasConversion(
                id => id.Id,
                value => new CharacterLevelId(value));

        builder
            .Property(charRequirement => charRequirement.ItemId)
            .HasConversion(
                id => id.Id,
                value => new ItemId(value));
    }
}
