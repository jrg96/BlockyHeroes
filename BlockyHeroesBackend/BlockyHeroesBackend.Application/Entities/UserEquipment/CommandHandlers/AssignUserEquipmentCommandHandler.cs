using BlockyHeroesBackend.Application.Abstractions;
using BlockyHeroesBackend.Application.Common;
using BlockyHeroesBackend.Application.Entities.UserEquipment.Commands;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using BlockyHeroesBackend.Domain.Repositories;
using BlockyHeroesBackend.Domain.Repositories.Command;
using BlockyHeroesBackend.Domain.Repositories.Query;

namespace BlockyHeroesBackend.Application.Entities.UserEquipment.CommandHandlers;

public class AssignUserEquipmentCommandHandler : IOperationHandler<AssignUserEquipmentCommand>
{
    private readonly IUserCharacterQueryRepository _userCharacterQueryRepository;
    private readonly IUserCharacterCommandRepository _userCharacterCommandRepository;
    private readonly IUserEquipmentQueryRepository _userEquipmentQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AssignUserEquipmentCommandHandler(
        IUserCharacterQueryRepository userCharacterQueryRepository,
        IUserCharacterCommandRepository userCharacterCommandRepository,
        IUserEquipmentQueryRepository userEquipmentQueryRepository,
        IUnitOfWork unitOfWork)
    {
        _userCharacterQueryRepository = userCharacterQueryRepository;
        _userCharacterCommandRepository = userCharacterCommandRepository;
        _userEquipmentQueryRepository = userEquipmentQueryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult> Handle(AssignUserEquipmentCommand request, CancellationToken cancellationToken)
    {
        UserId userId = new UserId(request.UserId);
        UserCharacterId userCharacterId = new UserCharacterId(request.UserCharacterId);
        UserEquipmentId userEquipmentId = new UserEquipmentId(request.UserEquipmentId);

        // Step 1: Verify the ownership of Equipment and Character
        Domain.Entities.User.UserCharacter? userCharacter =
            await _userCharacterQueryRepository.GetById(userCharacterId);

        Domain.Entities.User.UserEquipment? userEquipment =
            await _userEquipmentQueryRepository.GetById(userEquipmentId);

        if (!(userCharacter.Owner.Id == userId && userEquipment.Owner.Id == userId))
        {
            return OperationResult.GenericInvalidOperation;
        }

        /*
         * Equipment Assignment logic
         */

        // Step 2: Check if the equipment is already in use by another character
        // if so, this will trigger the old character have that equip removed
        Domain.Entities.User.UserCharacter selectedChar =
                userEquipment.UserCharacterSlot1 != null ?
                    userEquipment.UserCharacterSlot1
                    : userEquipment.UserCharacterSlot2 != null ?
                        userEquipment.UserCharacterSlot2
                        : null;

        if (selectedChar != null)
        {
            if (selectedChar.UserEquipmentIdSlot1 == userEquipment.Id)
            {
                selectedChar.UserEquipmentIdSlot1 = null;
                selectedChar.UserEquipmentSlot1 = null;

                userEquipment.UserCharacterSlot1 = null;

                await _userCharacterCommandRepository.UpdateAsync(selectedChar);
            }
            else if (selectedChar.UserEquipmentIdSlot2 == userEquipment.Id)
            {
                selectedChar.UserEquipmentIdSlot2 = null;
                selectedChar.UserEquipmentSlot2 = null;

                userEquipment.UserCharacterSlot2 = null;

                await _userCharacterCommandRepository.UpdateAsync(selectedChar);
            }
        }

        // Step 3: Apply equipment assign
        if (request.SlotToEquip == 1)
        {
            userCharacter.UserEquipmentIdSlot1 = userEquipment.Id;
            userCharacter.UserEquipmentSlot1 = userEquipment;
        }
        else if (request.SlotToEquip == 2)
        {
            userCharacter.UserEquipmentIdSlot2 = userEquipment.Id;
            userCharacter.UserEquipmentSlot2 = userEquipment;
        }

        // Apply Changes and save
        await _userCharacterCommandRepository.UpdateAsync(userCharacter);
        await _unitOfWork.SaveChangesAsync();

        return OperationResult.GenericSuccess;
    }
}
