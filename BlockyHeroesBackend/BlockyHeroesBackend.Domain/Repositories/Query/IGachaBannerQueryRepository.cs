using BlockyHeroesBackend.Domain.Common.ValueObjects.Banner;
using BlockyHeroesBackend.Domain.Entities.Banner;

namespace BlockyHeroesBackend.Domain.Repositories.Query;

public interface IGachaBannerQueryRepository : IGenericQueryRepository<GachaBanner>
{
    Task<GachaBanner?> GetByIdAsync(GachaBannerId gachaBannerId);
}
