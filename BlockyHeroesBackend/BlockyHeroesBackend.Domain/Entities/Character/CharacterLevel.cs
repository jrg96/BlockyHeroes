using BlockyHeroesBackend.Domain.Common.ValueObjects.Character;
using BlockyHeroesBackend.Domain.Entities.Banner;
using BlockyHeroesBackend.Domain.Entities.User;

namespace BlockyHeroesBackend.Domain.Entities.Character;

public class CharacterLevel
{
    public CharacterLevelId Id { get; set; }
    public int Level { get; set; }
    public int Lives { get; set; }
    public float JumpForce { get; set; }
    public float HorizontalSpeed { get; set; }

    // Foreign Key properties
    public CharacterId CharacterId { get; set; }
    public Character Character { get; set; }

    public ICollection<CharacterLevelRequirement> CharacterLevelRequirements { get; set; }
    public ICollection<UserCharacter> UserCharacters { get; set; }
    public ICollection<GachaBannerCharacter> GachaBannerCharacters { get; set; }
}
