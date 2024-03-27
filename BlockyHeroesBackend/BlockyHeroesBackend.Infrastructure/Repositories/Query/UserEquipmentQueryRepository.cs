using BlockyHeroesBackend.Domain.Common.ValueObjects.Equip;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using BlockyHeroesBackend.Domain.Entities.User;
using BlockyHeroesBackend.Domain.Repositories.Query;
using BlockyHeroesBackend.Infrastructure.Context;
using BlockyHeroesBackend.Infrastructure.Specifications.UserEquipment;
using Microsoft.EntityFrameworkCore;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Query;

public class UserEquipmentQueryRepository : GenericQueryRepository<UserEquipment>, IUserEquipmentQueryRepository
{
    public UserEquipmentQueryRepository(BlockyHeroesDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<UserEquipment>> GetByEquipLevelAndUser(UserId userId, EquipLevelId equipLevelId)
    {
        return await Find(new GetByEquipLevelAndUserSpecification(userId, equipLevelId))
            .ToListAsync();
    }

    public async Task<UserEquipment?> GetById(UserEquipmentId id)
    {
        return await Find(new GetByUserEquipmentIdSpecification(id))
            .FirstOrDefaultAsync();
    }
}
