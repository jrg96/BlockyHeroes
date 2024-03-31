using BlockyHeroesBackend.Domain.Common.ValueObjects.Common;

namespace BlockyHeroesBackend.Domain.Common.ValueObjects.Banner.RandomWeightedPicker;

public interface IWeighted
{
    public int Weight { get; set; }
    public ItemRarity ItemRarity { get; set; }
}
