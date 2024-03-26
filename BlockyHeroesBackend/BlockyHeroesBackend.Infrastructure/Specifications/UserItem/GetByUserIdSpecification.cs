using Ardalis.Specification;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;

namespace BlockyHeroesBackend.Infrastructure.Specifications.UserItem;

public class GetByUserIdSpecification : Specification<Domain.Entities.User.UserItem>
{
    public GetByUserIdSpecification(UserId userId)
    {
        Query
            .Include(userItem => userItem.Item)
            .Where(userItem => userItem.UserId == userId);
    }
}
