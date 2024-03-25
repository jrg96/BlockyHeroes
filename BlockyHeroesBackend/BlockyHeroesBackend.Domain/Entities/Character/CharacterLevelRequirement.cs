using BlockyHeroesBackend.Domain.Common.ValueObjects.Character;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Item;

namespace BlockyHeroesBackend.Domain.Entities.Character;

public class CharacterLevelRequirement
{
    public CharacterLevelRequirementId Id { get; set; }
    public int Quantity { get; set; }

    // Foreign Key configurations
    public CharacterLevelId CharacterLevelId { get; set; }
    public CharacterLevel CharacterLevel { get; set; }

    public ItemId ItemId { get; set; }
    public Item.Item Item { get; set; }
}
