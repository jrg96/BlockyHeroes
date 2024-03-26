using BlockyHeroesBackend.Application.Abstractions;
using BlockyHeroesBackend.Application.Common;
using BlockyHeroesBackend.Application.Entities.UserCharacter.Commands;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using BlockyHeroesBackend.Domain.Entities.Character;
using BlockyHeroesBackend.Domain.Entities.User;
using BlockyHeroesBackend.Domain.Repositories;
using BlockyHeroesBackend.Domain.Repositories.Command;
using BlockyHeroesBackend.Domain.Repositories.Query;

namespace BlockyHeroesBackend.Application.Entities.UserCharacter.CommandHandlers;

public class UpgradeUserCharacterCommandHandler : IOperationHandler<UpgradeUserCharacterCommand>
{
    private readonly IUserCharacterQueryRepository _userCharacterQueryRepository;
    private readonly IUserCharacterCommandRepository _userCharacterCommandRepository;
    private readonly IUserItemQueryRepository _userItemQueryRepository;
    private readonly IUserItemCommandRepository _userItemCommandRepository;
    private readonly ICharacterQueryRepository _characterQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpgradeUserCharacterCommandHandler(
        IUserCharacterQueryRepository userCharacterQueryRepository,
        IUserCharacterCommandRepository userCharacterCommandRepository,
        IUserItemQueryRepository userItemQueryRepository,
        IUserItemCommandRepository userItemCommandRepository,
        ICharacterQueryRepository characterQueryRepository,
        IUnitOfWork unitOfWork)
    {
        _userCharacterQueryRepository = userCharacterQueryRepository;
        _userCharacterCommandRepository = userCharacterCommandRepository;
        _userItemQueryRepository = userItemQueryRepository;
        _userItemCommandRepository = userItemCommandRepository;
        _characterQueryRepository = characterQueryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult> Handle(UpgradeUserCharacterCommand request, CancellationToken cancellationToken)
    {
        
        UserCharacterId userCharacterId = new UserCharacterId(request.UserCharacterId);
        UserId userId = new UserId(request.UserId);


        // Step 1: Check the ownership of the usercharacter to the corresponding user
        Domain.Entities.User.UserCharacter? userCharacter = await _userCharacterQueryRepository.GetById(userCharacterId);

        if (userCharacter == null || userCharacter?.Owner?.Id != userId)
        {
            return OperationResult.GenericInvalidOperation;
        }

        // Step 2: After ownership checking, verify if user has required amount of resources
        // Get Character to upgrade data and resources available from the user
        Character? selectedCharacter = 
            await _characterQueryRepository.GetByIdAsync(userCharacter.CharacterLevel.CharacterId);
        IEnumerable<UserItem> availableResources =
            await _userItemQueryRepository.GetByUserId(userId);

        IEnumerable<CharacterLevel> coveredCharacterLevels = selectedCharacter.CharacterLevels
            .Where(characterLevel => characterLevel.Level > userCharacter.CharacterLevel.Level)
            .OrderBy(characterLevel => characterLevel.Level)
            .Take(request.LevelsToUpgrade);

        IEnumerable<UserItem> requiredResources = coveredCharacterLevels
            .SelectMany(characterLevel => characterLevel.CharacterLevelRequirements)
            .GroupBy(charLevelReq => charLevelReq.ItemId)
            .Select(group => new UserItem()
            {
                ItemId = group.Key,
                Quantity = group.Sum(g => g.Quantity)
            });

        bool hasAllResources = requiredResources
            .All(userItem =>
            {
                return availableResources
                    .Any(ar => ar.ItemId == userItem.ItemId
                        && ar.Quantity >= userItem.Quantity);
            });

        if(!hasAllResources)
        {
            return OperationResult.GenericInvalidOperation;
        }

        // Step 3: After all verifications have been done, proceed with the character upgrade
        CharacterLevel? targetCharacterLevel = coveredCharacterLevels
            .LastOrDefault();

        userCharacter.CharacterLevelId = targetCharacterLevel.Id;
        userCharacter.CharacterLevel = targetCharacterLevel;
        await _userCharacterCommandRepository.UpdateAsync(userCharacter);

        foreach(UserItem availableResource in availableResources)
        {
            UserItem requiredResource = requiredResources
                .FirstOrDefault(reqResource => reqResource.Id == availableResource.Id);

            availableResource.Quantity -= requiredResource.Quantity;
            await _userItemCommandRepository.UpdateAsync(availableResource);
        }

        await _unitOfWork.SaveChangesAsync();
        return OperationResult.GenericSuccess;
    }
}
