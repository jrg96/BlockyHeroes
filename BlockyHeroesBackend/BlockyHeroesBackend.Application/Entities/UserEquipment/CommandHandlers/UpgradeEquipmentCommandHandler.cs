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
    private readonly IUserItemCommandRepository _userItemCommandRepository;
    private readonly IUserItemQueryRepository _userItemQueryRepository;

    private readonly IEquipQueryRepository _equipQueryRepository;
    private readonly IUserCommandRepository _userCommandRepository;

    public UpgradeEquipmentCommandHandler(
        IUnitOfWork unitOfWork,
        IUserEquipmentCommandRepository userEquipmentCommandRepository, 
        IUserEquipmentQueryRepository userEquipmentQueryRepository,
        IUserItemCommandRepository userItemCommandRepository,
        IUserItemQueryRepository userItemQueryRepository,
        IEquipQueryRepository equipQueryRepository,
        IUserCommandRepository userCommandRepository)
    {
        _userEquipmentCommandRepository = userEquipmentCommandRepository;
        _userEquipmentQueryRepository = userEquipmentQueryRepository;
        _userItemCommandRepository = userItemCommandRepository;
        _userItemQueryRepository = userItemQueryRepository;
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
            return OperationResult.GenericInvalidOperation;
        }

        // Step 2: After verifying the ownership, calculate if user has the corresponding resources
        Domain.Entities.User.User? user = userEquip.Owner;
        IEnumerable<UserItem> userItems = await _userItemQueryRepository.GetByUserId(user.Id);

        Equip? equip = await _equipQueryRepository.GetByEquipLevelId(new EquipLevelId(request.EquipLevelId));
        EquipLevel? currentLevelEquip = equip.EquipmentEvolutions
            .Where(equipLevel => equipLevel.Id == equipLevelId)
            .FirstOrDefault();
        IEnumerable<EquipLevel> nextLevelsEquips = equip.EquipmentEvolutions
            .Where(equipEvolution => equipEvolution.Level > currentLevelEquip.Level)
            .OrderBy(equipEvolution => equipEvolution.Level);

        if (request.Levels > nextLevelsEquips.Count())
        {
            return OperationResult.GenericInvalidOperation;
        }

        IEnumerable<UserItem> requiredResources = nextLevelsEquips
            .Take(request.Levels)
            .SelectMany(equipLevel => equipLevel.EquipLevelRequirements)
            .GroupBy(requirement => requirement.Item.Id)
            .Select(group => new UserItem
            {
                ItemId = group.Key,
                Quantity = group.Sum(gr => gr.Quantity)
            })
            .ToList();

        bool hasEnoughResources = requiredResources
            .All(resource =>
            {
                return userItems
                    .Any(userItem => userItem.ItemId == resource.ItemId
                        && userItem.Quantity >= resource.Quantity);
            });

        if (!hasEnoughResources)
        {
            return OperationResult.GenericInvalidOperation;
        }

        // Step 3: After we have validated user has the ownership of the item
        // and the resources to complete the enhacement, we proceed with the transaction
        foreach (UserItem requiredResource in requiredResources)
        {
            UserItem userResource = userItems
                .First(userItem => userItem.ItemId == requiredResource.ItemId);
            userResource.Quantity -= requiredResource.Quantity;
            await _userItemCommandRepository.UpdateAsync(userResource);
        }

        // Verify if user already has the equipment at the new level
        // and if not, create a new entity
        EquipLevel desiredEquip = nextLevelsEquips
            .Take(request.Levels)
            .Last();

        // Update current equip to point to the next level EquipLevel
        userEquip.EquipLevel = desiredEquip;
        userEquip.EquipLevelId = desiredEquip.Id;

        // Execute the operations
        await _userEquipmentCommandRepository.UpdateAsync(userEquip);
        await _unitOfWork.SaveChangesAsync();

        return new OperationResult()
        {
            Success = true,
        };
    }
}
