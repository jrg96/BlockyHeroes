using BlockyHeroesBackend.Domain.Common.ValueObjects.Banner;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Item;
using BlockyHeroesBackend.Domain.Entities.Item;

namespace BlockyHeroesBackend.Domain.Entities.Banner;

public class GachaBannerCurrency
{
    public GachaBannerCurrencyId Id { get; set; }
    public int QuantityPerPull { get; set; } // Amount of currency to spend per pull

    // Foreign Key references
    public GachaBannerId GachaBannerId { get; set; }
    public GachaBanner GachaBanner { get; set; }

    public ItemId ItemId { get; set; }
    public Item.Item Item { get; set; }
}
