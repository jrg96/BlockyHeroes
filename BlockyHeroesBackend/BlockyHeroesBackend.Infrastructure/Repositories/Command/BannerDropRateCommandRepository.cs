using BlockyHeroesBackend.Domain.Entities.Banner;
using BlockyHeroesBackend.Domain.Repositories.Command;
using BlockyHeroesBackend.Infrastructure.Context;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Command;

public class BannerDropRateCommandRepository : GenericCommandRepository<BannerDropRate>, IBannerDropRateCommandRepository
{
    public BannerDropRateCommandRepository(BlockyHeroesDbContext context) : base(context)
    {
    }
}
