using BlockyHeroesBackend.Application.Services;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Banner;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Banner.RandomWeightedPicker;

namespace BlockyHeroesBackend.Infrastructure.Services;

public class GachaBannerCacheService : IGachaBannerCacheService
{
    private Dictionary<GachaBannerId, RandomWeightedPicker<WeightedCharacter>> _banners = new Dictionary<GachaBannerId, RandomWeightedPicker<WeightedCharacter>>();

    public RandomWeightedPicker<WeightedCharacter> GetBanner(GachaBannerId bannerId)
    {
        return _banners[bannerId];
    }

    public bool IsBannerLoaded(GachaBannerId bannerId)
    {
        return _banners.ContainsKey(bannerId);
    }

    public void SaveBanner(GachaBannerId bannerId, RandomWeightedPicker<WeightedCharacter> bannerData)
    {
        _banners.Add(bannerId, bannerData);
    }
}
