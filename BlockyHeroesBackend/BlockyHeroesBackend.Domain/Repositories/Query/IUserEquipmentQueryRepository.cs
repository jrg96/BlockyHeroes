using BlockyHeroesBackend.Domain.Common.ValueObjects.Equip;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using BlockyHeroesBackend.Domain.Entities.User;

namespace BlockyHeroesBackend.Domain.Repositories.Query;

public interface IUserEquipmentQueryRepository : IGenericQueryRepository<UserEquipment>
{
    Task<UserEquipment?> GetById(UserEquipmentId id);
    Task<IEnumerable<UserEquipment>> GetByIdBulk(IEnumerable<UserEquipmentId> userEquipmentIds);
    Task<IEnumerable<UserEquipment>> GetByEquipLevelAndUser(UserId userId, EquipLevelId equipLevelId);
}
