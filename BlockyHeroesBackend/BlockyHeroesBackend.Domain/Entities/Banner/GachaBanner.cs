using BlockyHeroesBackend.Domain.Common.ValueObjects.Banner;

namespace BlockyHeroesBackend.Domain.Entities.Banner;

public class GachaBanner
{
    public GachaBannerId Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public BannerType Type { get; set; }

    // Foreign key references
    public ICollection<BannerDropRate> DropRates { get; set; }
    public ICollection<GachaBannerCharacter> GachaBannerCharacters { get; set; }
}
