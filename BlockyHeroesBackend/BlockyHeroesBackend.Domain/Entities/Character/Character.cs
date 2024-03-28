using BlockyHeroesBackend.Domain.Common.ValueObjects.Character;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Common;

namespace BlockyHeroesBackend.Domain.Entities.Character;

public class Character
{
    public CharacterId Id { get; set; }
    public ItemRarity Rarity { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    // Foreign Key properties
    public ICollection<CharacterLevel> CharacterLevels { get; set; }
}
