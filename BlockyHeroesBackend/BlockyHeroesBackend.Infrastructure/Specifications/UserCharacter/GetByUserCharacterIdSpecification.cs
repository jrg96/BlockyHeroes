using Ardalis.Specification;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;

namespace BlockyHeroesBackend.Infrastructure.Specifications.UserCharacter;

public class GetByUserCharacterIdSpecification : Specification<Domain.Entities.User.UserCharacter>
{
    public GetByUserCharacterIdSpecification(UserCharacterId id)
    {
        Query
            .Include(userChar => userChar.Owner)
            .Include(userChar => userChar.CharacterLevel)
            .Include(userChar => userChar.UserEquipmentSlot1)
            .Include(userChar => userChar.UserEquipmentSlot2)
            .Where(userChar => userChar.Id == id);
    }
}
