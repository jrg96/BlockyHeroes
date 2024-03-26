using BlockyHeroesBackend.Domain.Common.ValueObjects.Equip;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using BlockyHeroesBackend.Domain.Entities.Equip;

namespace BlockyHeroesBackend.Domain.Entities.User;

public class UserEquipment
{
    public UserEquipmentId Id { get; set; }

    // Foreign Key relationships
    public UserId UserId { get; set; }
    public User Owner { get; set; }

    public EquipLevelId EquipLevelId { get; set; }
    public EquipLevel EquipLevel { get; set; }

    public UserCharacter UserCharacterSlot1 { get; set; } // An Equip can appear on slot 1 or 2
    public UserCharacter UserCharacterSlot2 { get; set; } // An Equip can appear on slot 1 or 2
}
