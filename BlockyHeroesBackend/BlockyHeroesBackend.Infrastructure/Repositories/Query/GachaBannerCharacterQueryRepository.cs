using BlockyHeroesBackend.Domain.Entities.Banner;
using BlockyHeroesBackend.Domain.Repositories.Query;
using BlockyHeroesBackend.Infrastructure.Context;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Query;

public class GachaBannerCharacterQueryRepository : GenericQueryRepository<GachaBannerCharacter>, IGachaBannerCharacterQueryRepository
{
    public GachaBannerCharacterQueryRepository(BlockyHeroesDbContext context) : base(context)
    {
    }
}
