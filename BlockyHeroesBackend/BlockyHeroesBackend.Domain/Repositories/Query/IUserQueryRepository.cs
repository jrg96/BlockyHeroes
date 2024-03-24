using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using BlockyHeroesBackend.Domain.Entities.User;

namespace BlockyHeroesBackend.Domain.Repositories.Query;

public interface IUserQueryRepository : IGenericQueryRepository<User>
{
    Task<User?> GetByIdAsync(UserId userId);
}
