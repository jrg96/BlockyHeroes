using Ardalis.Specification;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using BlockyHeroesBackend.Domain.Entities.User;

namespace BlockyHeroesBackend.Infrastructure.Specifications.UserEquipment;

public class GetByUserEquipmentIdSpecification : Specification<Domain.Entities.User.UserEquipment>
{
    public GetByUserEquipmentIdSpecification(UserEquipmentId userEquipmentId)
    {
        Query
            .Include(userEquipment => userEquipment.Owner)
            .Include(userEquipment => userEquipment.UserCharacterSlot1)
            .Include(userEquipment => userEquipment.UserCharacterSlot2)
            .Where(userEquipment => userEquipment.Id == userEquipmentId);
    }
}
