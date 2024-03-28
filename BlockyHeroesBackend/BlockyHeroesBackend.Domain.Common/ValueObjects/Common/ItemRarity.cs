namespace BlockyHeroesBackend.Domain.Common.ValueObjects.Common;

public record ItemRarity(int Id, string Name)
{
    public static ItemRarity SuperSuperRare { get; } = new ItemRarity(1, "Super Super rare");
    public static ItemRarity SuperRare { get; } = new ItemRarity(2, "Super Rare");
    public static ItemRarity Rare { get; } = new ItemRarity(3, "Rare");
    public static ItemRarity Common { get; } = new ItemRarity(4, "Common");

    public static ItemRarity Get(int id)
    {
        if (SuperSuperRare.Id == id)
        {
            return SuperSuperRare;
        }
        else if(SuperRare.Id == id)
        {
            return SuperRare;
        }
        else if(Rare.Id == id)
        {
            return Rare;
        }
        else
        {
            return Common;
        }
    }
}
