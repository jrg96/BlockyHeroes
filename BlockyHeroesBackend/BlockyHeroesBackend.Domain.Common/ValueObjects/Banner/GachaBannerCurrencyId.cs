namespace BlockyHeroesBackend.Domain.Common.ValueObjects.Banner;

public readonly record struct GachaBannerCurrencyId(Guid Id)
{
    public static GachaBannerCurrencyId Empty => new GachaBannerCurrencyId(Guid.Empty);
    public static GachaBannerCurrencyId CreateGachaBannerCurrencyId()
    {
        return new GachaBannerCurrencyId(Guid.NewGuid());
    }
}
