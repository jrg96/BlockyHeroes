using BlockyHeroesBackend.Domain.Common.ValueObjects.Banner;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Common;

namespace BlockyHeroesBackend.Domain.Entities.Banner;

public class BannerDropRate
{
    public BannerDropRateId Id { get; set; }
    public ItemRarity Rarity { get; set; }
    public float DropRate { get; set; }

    // Foreign Key references
    public GachaBannerId GachaBannerId { get; set; }
    public GachaBanner GachaBanner { get; set; }
}
