using Ardalis.Specification;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;

namespace BlockyHeroesBackend.Infrastructure.Specifications.User;

public class GetUserByUserIdSpecification : Specification<Domain.Entities.User.User>
{
    public GetUserByUserIdSpecification(UserId userId)
    {
        Query
            .Include(user => user.UserItems)
                .ThenInclude(userItem => userItem.Item)
            .Where(user => user.Id == userId);
    }
}
