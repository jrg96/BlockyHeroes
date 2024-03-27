using BlockyHeroesBackend.Application.Abstractions;
using BlockyHeroesBackend.Application.Common;
using BlockyHeroesBackend.Application.Entities.UserEquipment.Commands;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using BlockyHeroesBackend.Domain.Entities.User;
using BlockyHeroesBackend.Domain.Repositories;
using BlockyHeroesBackend.Domain.Repositories.Command;
using BlockyHeroesBackend.Domain.Repositories.Query;

namespace BlockyHeroesBackend.Application.Entities.UserEquipment.CommandHandlers;

public class DestroyUserEquipmentCommandHandler : IOperationHandler<DestroyUserEquipmentCommand>
{
    private readonly IUserEquipmentCommandRepository _userEquipmentCommandRepository;
    private readonly IUserEquipmentQueryRepository _userEquipmentQueryRepository;
    private readonly IUserCharacterCommandRepository _userCharacterCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DestroyUserEquipmentCommandHandler(
        IUserEquipmentCommandRepository userEquipmentCommandRepository,
        IUserEquipmentQueryRepository userEquipmentQueryRepository,
        IUserCharacterCommandRepository userCharacterCommandRepository,
        IUnitOfWork unitOfWork)
    {
        _userEquipmentCommandRepository = userEquipmentCommandRepository;
        _userEquipmentQueryRepository = userEquipmentQueryRepository;
        _userCharacterCommandRepository = userCharacterCommandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult> Handle(DestroyUserEquipmentCommand request, CancellationToken cancellationToken)
    {
        IEnumerable<UserEquipmentId> userEquipmentIds = request.UserEquipmentIds
            .Select(id => new UserEquipmentId(id))
            .ToList();

        UserId userId = new UserId(request.UserId);

        // Step 1: Verify the ownership of the equipment
        IEnumerable<Domain.Entities.User.UserEquipment> userEquipments =
            await _userEquipmentQueryRepository.GetByIdBulk(userEquipmentIds);

        if (!userEquipments.Any() || !userEquipments.All(userEquipment => userEquipment.Owner.Id == userId))
        {
            return OperationResult.GenericInvalidOperation;
        }

        // Step 2: Perform the delete (if equipment was assigned to a character)
        // it will automatically set to null the Slot of the character
        userEquipments.ToList().ForEach(async userEquip =>
        {
            if (userEquip.UserCharacterSlot1 != null)
            {
                userEquip.UserCharacterSlot1.UserEquipmentIdSlot1 = null;
                await _userCharacterCommandRepository.UpdateAsync(userEquip.UserCharacterSlot1);
            }
            if (userEquip.UserCharacterSlot2 != null)
            {
                userEquip.UserCharacterSlot2.UserEquipmentIdSlot2 = null;
                await _userCharacterCommandRepository.UpdateAsync(userEquip.UserCharacterSlot1);
            }

            await _userEquipmentCommandRepository.DeleteAsync(userEquip);
        });
        await _unitOfWork.SaveChangesAsync();

        return OperationResult.GenericSuccess;
    }
}
