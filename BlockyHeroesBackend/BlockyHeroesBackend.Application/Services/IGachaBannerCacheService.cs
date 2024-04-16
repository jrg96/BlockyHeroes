using BlockyHeroesBackend.Domain.Common.ValueObjects.Banner;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Banner.RandomWeightedPicker;

namespace BlockyHeroesBackend.Application.Services;

public interface IGachaBannerCacheService
{
    bool IsBannerLoaded(GachaBannerId bannerId);
    RandomWeightedPicker<WeightedCharacter> GetBanner(GachaBannerId bannerId);
    void SaveBanner(GachaBannerId bannerId, RandomWeightedPicker<WeightedCharacter> bannerData);
}
