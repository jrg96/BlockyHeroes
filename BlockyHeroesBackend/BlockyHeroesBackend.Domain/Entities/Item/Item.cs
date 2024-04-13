using BlockyHeroesBackend.Domain.Common.ValueObjects.Item;
using BlockyHeroesBackend.Domain.Entities.Banner;
using BlockyHeroesBackend.Domain.Entities.Character;
using BlockyHeroesBackend.Domain.Entities.Equip;
using BlockyHeroesBackend.Domain.Entities.User;

namespace BlockyHeroesBackend.Domain.Entities.Item;

public class Item
{
    public ItemId Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ItemTypes Type { get; set; }

    // Foreign Key relationships
    public ICollection<UserItem> UserItems { get; set; }
    public ICollection<CharacterLevelRequirement> CharacterLevelRequirements { get; set; }
    public ICollection<EquipLevelRequirement> EquipLevelRequirements { get; set; }
    public ICollection<GachaBannerCurrency> GachaBannerCurrencies { get; set; }
}
