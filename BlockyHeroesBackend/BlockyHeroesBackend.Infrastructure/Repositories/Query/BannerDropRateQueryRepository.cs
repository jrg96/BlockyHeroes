using BlockyHeroesBackend.Domain.Entities.Banner;
using BlockyHeroesBackend.Domain.Repositories.Query;
using BlockyHeroesBackend.Infrastructure.Context;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Query;

public class BannerDropRateQueryRepository : GenericQueryRepository<BannerDropRate>, IBannerDropRateQueryRepository
{
    public BannerDropRateQueryRepository(BlockyHeroesDbContext context) : base(context)
    {
    }
}
