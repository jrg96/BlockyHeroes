using BlockyHeroesBackend.Domain.Common.ValueObjects.Equip;
using BlockyHeroesBackend.Domain.Entities.Equip;

namespace BlockyHeroesBackend.Domain.Repositories.Query;

public interface IEquipQueryRepository : IGenericQueryRepository<Equip>
{
    Task<Equip?> GetByEquipLevelId(EquipLevelId equipLevelId);
    Task<IEnumerable<Equip>> GetAll();
}
