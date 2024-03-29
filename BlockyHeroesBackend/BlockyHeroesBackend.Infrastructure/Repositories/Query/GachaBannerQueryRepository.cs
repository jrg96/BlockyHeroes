using BlockyHeroesBackend.Domain.Common.ValueObjects.Banner;
using BlockyHeroesBackend.Domain.Entities.Banner;
using BlockyHeroesBackend.Domain.Repositories.Query;
using BlockyHeroesBackend.Infrastructure.Context;
using BlockyHeroesBackend.Infrastructure.Specifications.GachaBanner;
using Microsoft.EntityFrameworkCore;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Query;

public class GachaBannerQueryRepository : GenericQueryRepository<GachaBanner>, IGachaBannerQueryRepository
{
    public GachaBannerQueryRepository(BlockyHeroesDbContext context) : base(context)
    {
    }

    public async Task<GachaBanner?> GetByIdAsync(GachaBannerId gachaBannerId)
    {
        return await Find(new GetByGachaBannerIdSpecification(gachaBannerId))
            .FirstOrDefaultAsync();
    }
}
