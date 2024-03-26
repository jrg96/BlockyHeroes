using BlockyHeroesBackend.Application.Abstractions;
using BlockyHeroesBackend.Application.Common;
using BlockyHeroesBackend.Application.Entities.UserEquipment.Commands;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Common;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Equip;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using BlockyHeroesBackend.Domain.Entities.Equip;
using BlockyHeroesBackend.Domain.Entities.User;
using BlockyHeroesBackend.Domain.Repositories;
using BlockyHeroesBackend.Domain.Repositories.Command;
using BlockyHeroesBackend.Domain.Repositories.Query;

namespace BlockyHeroesBackend.Application.Entities.UserEquipment.CommandHandlers;

public class UpgradeEquipmentCommandHandler : IOperationHandler<UpgradeEquipmentCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IUserEquipmentCommandRepository _userEquipmentCommandRepository;
    private readonly IUserEquipmentQueryRepository _userEquipmentQueryRepository;

    private readonly IEquipQueryRepository _equipQueryRepository;
    private readonly IUserCommandRepository _userCommandRepository;

    public UpgradeEquipmentCommandHandler(
        IUnitOfWork unitOfWork,
        IUserEquipmentCommandRepository userEquipmentCommandRepository, 
        IUserEquipmentQueryRepository userEquipmentQueryRepository, 
        IEquipQueryRepository equipQueryRepository,
        IUserCommandRepository userCommandRepository)
    {
        _userEquipmentCommandRepository = userEquipmentCommandRepository;
        _userEquipmentQueryRepository = userEquipmentQueryRepository;
        _equipQueryRepository = equipQueryRepository;
        _userCommandRepository = userCommandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult> Handle(UpgradeEquipmentCommand request, CancellationToken cancellationToken)
    {
        UserId userId = new UserId(request.UserId);
        EquipLevelId equipLevelId = new EquipLevelId(request.EquipLevelId);

        // Step 1: Verify if the user has the ownership of the equipment + its level
        // NOTE: There's no distinction between equips of the same level, so take the first one
        Domain.Entities.User.UserEquipment? userEquip = 
            (await _userEquipmentQueryRepository.GetByEquipLevelAndUser(userId, equipLevelId))
            .FirstOrDefault();

        if (userEquip == null)
        {
            return GenericInvalidResponse();
        }

        // Step 2: After verifying the ownership, calculate if user has the corresponding resources
        Domain.Entities.User.User? user = userEquip.Owner;
        Equip? equip = await _equipQueryRepository.GetByEquipLevelId(new EquipLevelId(request.EquipLevelId));
        EquipLevel? currentLevelEquip = equip.EquipmentEvolutions
            .Where(equipLevel => equipLevel.Id == equipLevelId)
            .FirstOrDefault();
        IEnumerable<EquipLevel> nextLevelsEquips = equip.EquipmentEvolutions
            .Where(equipEvolution => equipEvolution.Level > currentLevelEquip.Level)
            .OrderBy(equipEvolution => equipEvolution.Level);

        if (request.Levels > nextLevelsEquips.Count())
        {
            return GenericInvalidResponse();
        }

        long resourcesToUse = nextLevelsEquips
            .Take(request.Levels)
            .Sum(levelEquip => levelEquip.CoinsToPromote);

        if (user.Coins < resourcesToUse)
        {
            return GenericInvalidResponse();
        }

        // Step 3: After we have validated user has the ownership of the item
        // and the resources to complete the enhacement, we proceed with the transaction
        user.Coins -= resourcesToUse;

        // Verify if user already has the equipment at the new level
        // and if not, create a new entity
        EquipLevel desiredEquip = nextLevelsEquips
            .Take(request.Levels)
            .Last();

        // Update current equip to point to the next level EquipLevel
        userEquip.EquipLevel = desiredEquip;
        userEquip.EquipLevelId = desiredEquip.Id;

        // Execute the operations
        await _userCommandRepository.UpdateAsync(user);
        await _userEquipmentCommandRepository.UpdateAsync(userEquip);
        await _unitOfWork.SaveChangesAsync();

        return new OperationResult()
        {
            Success = true,
        };
    }

    private OperationResult GenericInvalidResponse()
    {
        return new OperationResult()
        {
            Success = false,
            Errors = new List<Error>() { new Error(2, "Invalid Operation") }
        };
    }
}
