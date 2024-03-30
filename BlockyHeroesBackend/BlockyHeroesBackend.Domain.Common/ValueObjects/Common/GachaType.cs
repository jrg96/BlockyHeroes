namespace BlockyHeroesBackend.Domain.Common.ValueObjects.Common;

public record GachaType(int Id, string Name)
{
    public static GachaType PermanentPool { get; } = new GachaType(1, "Permanent Pool");
    public static GachaType ExclusiveGachaBanner { get; } = new GachaType(2, "Exclusive Gacha Banner");

    public static GachaType Get(int id)
    {
        if (id == ExclusiveGachaBanner.Id)
        {
            return ExclusiveGachaBanner;
        }

        return PermanentPool;
    }
}
