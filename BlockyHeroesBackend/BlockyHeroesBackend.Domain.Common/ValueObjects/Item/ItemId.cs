namespace BlockyHeroesBackend.Domain.Common.ValueObjects.Item;

public readonly record struct ItemId(Guid Id)
{
    public static ItemId Empty => new ItemId(Guid.Empty);

    public static ItemId CreateItemId()
    {
        return new ItemId(Guid.NewGuid());
    }
}
