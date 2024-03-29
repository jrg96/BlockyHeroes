namespace BlockyHeroesBackend.Domain.Common.ValueObjects.Item;

public record ItemTypes(int Id, string Name)
{
    public static ItemTypes Consumable { get; } = new ItemTypes(1, "Consumable");
    public static ItemTypes Static { get; } = new ItemTypes(2, "Static");
    public static ItemTypes GachaCurrency { get; } = new ItemTypes(3, "Gacha Currency");

    public static ItemTypes Get(int id)
    {
        if (id == Consumable.Id)
        {
            return Consumable;
        }
        else if (id == GachaCurrency.Id)
        {
            return GachaCurrency;
        }

        return Static;
    }
}
