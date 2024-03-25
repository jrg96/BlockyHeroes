using BlockyHeroesBackend.Domain.Entities.User;
using BlockyHeroesBackend.Domain.Repositories.Command;
using BlockyHeroesBackend.Infrastructure.Context;

namespace BlockyHeroesBackend.Infrastructure.Repositories.Command;

public class UserEquipmentCommandRepository : GenericCommandRepository<UserEquipment>, IUserEquipmentCommandRepository
{
    public UserEquipmentCommandRepository(BlockyHeroesDbContext context) : base(context)
    {
    }
}
