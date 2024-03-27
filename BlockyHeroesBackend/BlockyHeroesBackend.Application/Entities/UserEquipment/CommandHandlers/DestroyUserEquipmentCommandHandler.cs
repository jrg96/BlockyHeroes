using BlockyHeroesBackend.Application.Abstractions;
using BlockyHeroesBackend.Application.Common;
using BlockyHeroesBackend.Application.Entities.UserEquipment.Commands;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using BlockyHeroesBackend.Domain.Repositories;
using BlockyHeroesBackend.Domain.Repositories.Command;
using BlockyHeroesBackend.Domain.Repositories.Query;

namespace BlockyHeroesBackend.Application.Entities.UserEquipment.CommandHandlers;

public class DestroyUserEquipmentCommandHandler : IOperationHandler<DestroyUserEquipmentCommand>
{
    private readonly IUserEquipmentCommandRepository _userEquipmentCommandRepository;
    private readonly IUserEquipmentQueryRepository _userEquipmentQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DestroyUserEquipmentCommandHandler(
        IUserEquipmentCommandRepository userEquipmentCommandRepository,
        IUserEquipmentQueryRepository userEquipmentQueryRepository,
        IUnitOfWork unitOfWork)
    {
        _userEquipmentCommandRepository = userEquipmentCommandRepository;
        _userEquipmentQueryRepository = userEquipmentQueryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult> Handle(DestroyUserEquipmentCommand request, CancellationToken cancellationToken)
    {
        UserEquipmentId userEquipmentId = new UserEquipmentId(request.UserEquipmentId);
        UserId userId = new UserId(request.UserId);

        // Step 1: Verify the ownership of the equipment
        Domain.Entities.User.UserEquipment? userEquipment =
            await _userEquipmentQueryRepository.GetById(userEquipmentId);

        if (!(userEquipment?.Owner?.Id == userId))
        {
            return OperationResult.GenericInvalidOperation;
        }

        // Step 2: Perform the delete (if equipment was assigned to a character)
        // it will automatically set to null the Slot of the character

        await _userEquipmentCommandRepository.DeleteAsync(userEquipment);
        await _unitOfWork.SaveChangesAsync();

        return OperationResult.GenericSuccess;
    }
}
