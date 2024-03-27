using BlockyHeroesBackend.Application.Entities.UserEquipment.Commands;
using FluentValidation;

namespace BlockyHeroesBackend.Application.Entities.UserEquipment.Validators;

public class AssignUserEquipmentCommandValidator : AbstractValidator<AssignUserEquipmentCommand>
{
    public AssignUserEquipmentCommandValidator()
    {
        RuleFor(userEquip => userEquip.UserId)
            .NotNull();

        RuleFor(userEquip => userEquip.UserCharacterId)
            .NotNull();

        RuleFor(userEquip => userEquip.UserEquipmentId)
            .NotNull();

        RuleFor(userEquip => userEquip.SlotToEquip)
            .Must(IsValidSlot)
            .WithMessage("You must select slot 1 or 2");
    }

    private bool IsValidSlot(int slotNumber)
    {
        return slotNumber == 1 || slotNumber == 2;
    }
}
