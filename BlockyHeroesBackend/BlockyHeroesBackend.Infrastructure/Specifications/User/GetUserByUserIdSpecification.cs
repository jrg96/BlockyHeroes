using Ardalis.Specification;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;

namespace BlockyHeroesBackend.Infrastructure.Specifications.User;

public class GetUserByUserIdSpecification : Specification<Domain.Entities.User>
{
    public GetUserByUserIdSpecification(UserId userId)
    {
        Query
            .Where(user => user.Id == userId);
    }
}
