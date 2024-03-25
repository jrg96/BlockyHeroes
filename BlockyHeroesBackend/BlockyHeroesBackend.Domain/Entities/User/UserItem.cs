using BlockyHeroesBackend.Domain.Common.ValueObjects.Item;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;

namespace BlockyHeroesBackend.Domain.Entities.User;

public class UserItem
{
    public UserItemId Id { get; set; }
    public int Quantity { get; set; }

    // Foreign Key properties
    public UserId UserId { get; set; }
    public User User { get; set; }

    public ItemId ItemId { get; set; }
    public Item.Item Item { get; set; }
}
