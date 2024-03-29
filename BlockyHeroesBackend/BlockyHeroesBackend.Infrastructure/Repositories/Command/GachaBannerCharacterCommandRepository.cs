using BlockyHeroesBackend.Domain.Entities.Banner;
using BlockyHeroesBackend.Domain.Repositories.Command;
using BlockyHeroesBackend.Infrastructure.Context;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Command;

public class GachaBannerCharacterCommandRepository : GenericCommandRepository<GachaBannerCharacter>, IGachaBannerCharacterCommandRepository
{
    public GachaBannerCharacterCommandRepository(BlockyHeroesDbContext context) : base(context)
    {
    }
}
