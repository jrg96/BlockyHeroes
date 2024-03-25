using BlockyHeroesBackend.Domain.Common.ValueObjects.Equip;
using BlockyHeroesBackend.Domain.Entities.Equip;
using BlockyHeroesBackend.Domain.Repositories.Query;
using BlockyHeroesBackend.Infrastructure.Context;
using BlockyHeroesBackend.Infrastructure.Specifications.Equip;
using Microsoft.EntityFrameworkCore;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Query;

public class EquipQueryRepository : GenericQueryRepository<Equip>, IEquipQueryRepository
{
    public EquipQueryRepository(BlockyHeroesDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Equip>> GetAllAsync()
    {
        return await Find(new GetAllSpecification())
            .ToListAsync();
    }

    public async Task<Equip?> GetByEquipLevelId(EquipLevelId equipLevelId)
    {
        return await Find(new GetByEquipLevelIdSpecification(equipLevelId))
            .FirstOrDefaultAsync();
    }
}
