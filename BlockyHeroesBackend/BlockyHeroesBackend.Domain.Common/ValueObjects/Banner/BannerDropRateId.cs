namespace BlockyHeroesBackend.Domain.Common.ValueObjects.Banner;

public readonly record struct BannerDropRateId(Guid Id)
{
    public static BannerDropRateId Empty => new BannerDropRateId(Guid.Empty);
    public static BannerDropRateId CreateBannerDropRateId()
    {
        return new BannerDropRateId(Guid.NewGuid());
    }
}
