using BlockyHeroesBackend.Domain.Entities.Equip;
using BlockyHeroesBackend.Domain.Repositories.Command;
using BlockyHeroesBackend.Infrastructure.Context;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Command;

public class EquipCommandRepository : GenericCommandRepository<Equip>, IEquipCommandRepository
{
    public EquipCommandRepository(BlockyHeroesDbContext context) : base(context)
    {
    }
}
