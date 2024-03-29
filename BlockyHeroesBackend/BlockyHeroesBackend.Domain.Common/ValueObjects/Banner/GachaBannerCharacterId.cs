namespace BlockyHeroesBackend.Domain.Common.ValueObjects.Banner;

public readonly record struct GachaBannerCharacterId(Guid Id)
{
    public static GachaBannerCharacterId Empty => new GachaBannerCharacterId(Guid.Empty);

    public static GachaBannerCharacterId CreateGachaBannerCharacterId()
    {
        return new GachaBannerCharacterId(Guid.NewGuid());
    }
}
