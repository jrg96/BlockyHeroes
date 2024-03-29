using BlockyHeroesBackend.Domain.Entities.Banner;
using BlockyHeroesBackend.Domain.Repositories.Command;
using BlockyHeroesBackend.Infrastructure.Context;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Command;

public class GachaBannerCommandRepository : GenericCommandRepository<GachaBanner>, IGachaBannerCommandRepository
{
    public GachaBannerCommandRepository(BlockyHeroesDbContext context) : base(context)
    {
    }
}
