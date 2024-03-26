using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using BlockyHeroesBackend.Domain.Entities.User;

namespace BlockyHeroesBackend.Domain.Repositories.Query;

public interface IUserItemQueryRepository : IGenericQueryRepository<UserItem>
{
    Task<IEnumerable<UserItem>> GetByUserId(UserId userId);
}
