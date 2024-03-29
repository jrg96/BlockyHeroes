using BlockyHeroesBackend.Domain.Entities.Banner;
using BlockyHeroesBackend.Domain.Repositories.Query;
using BlockyHeroesBackend.Infrastructure.Context;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Query;

public class GachaBannerQueryRepository : GenericQueryRepository<GachaBanner>, IGachaBannerQueryRepository
{
    public GachaBannerQueryRepository(BlockyHeroesDbContext context) : base(context)
    {
    }
}
