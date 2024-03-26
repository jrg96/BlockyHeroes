using BlockyHeroesBackend.Domain.Common.ValueObjects.Character;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using BlockyHeroesBackend.Domain.Entities.Character;

namespace BlockyHeroesBackend.Domain.Entities.User;

public class UserCharacter
{
    public UserCharacterId Id { get; set; }

    // Foreign Key references
    public UserId UserId { get; set; } // This record belongs to a user
    public User Owner { get; set; }

    public CharacterLevelId CharacterLevelId { get; set; } // The user has a character aty certain level
    public CharacterLevel CharacterLevel { get; set; }

    public UserEquipmentId? UserEquipmentIdSlot1 { get; set; } // The character can have a equip in slot 1
    public UserEquipment UserEquipmentSlot1 { get; set; }

    public UserEquipmentId? UserEquipmentIdSlot2 { get; set; } // The character can have a equip in slot 2
    public UserEquipment UserEquipmentSlot2 { get; set; }
}
