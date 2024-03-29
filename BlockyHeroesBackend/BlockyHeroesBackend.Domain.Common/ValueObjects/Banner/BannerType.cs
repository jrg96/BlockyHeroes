namespace BlockyHeroesBackend.Domain.Common.ValueObjects.Banner;

public record BannerType(int Id, string Name)
{
    public static BannerType CharacterBanner { get; } = new BannerType(1, "Character");
    public static BannerType EquipmentBanner { get; } = new BannerType(2, "Equipment");

    public static BannerType Get(int id)
    {
        if (id == CharacterBanner.Id)
        {
            return CharacterBanner;
        }
        else
        {
            return EquipmentBanner;
        }
    }
}
