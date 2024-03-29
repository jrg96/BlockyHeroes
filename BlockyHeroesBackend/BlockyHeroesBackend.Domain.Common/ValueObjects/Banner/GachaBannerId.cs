namespace BlockyHeroesBackend.Domain.Common.ValueObjects.Banner;

public readonly record struct GachaBannerId(Guid Id)
{
    public static GachaBannerId Empty => new GachaBannerId(Guid.Empty);
    public static GachaBannerId CreateBannerId()
    {
        return new GachaBannerId(Guid.NewGuid());
    }
}
