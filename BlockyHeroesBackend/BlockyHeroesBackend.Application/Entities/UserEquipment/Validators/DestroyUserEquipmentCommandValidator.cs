using BlockyHeroesBackend.Application.Entities.UserEquipment.Commands;
using FluentValidation;

namespace BlockyHeroesBackend.Application.Entities.UserEquipment.Validators;

public class DestroyUserEquipmentCommandValidator : AbstractValidator<DestroyUserEquipmentCommand>
{
    public DestroyUserEquipmentCommandValidator()
    {
        RuleFor(userEquip => userEquip.UserId)
            .NotNull();

        RuleFor(userEquip => userEquip.UserEquipmentIds)
            .NotNull()
            .Must(NotEmptyArray)
            .WithMessage("UserEquipmentIds must contain elements");
    }

    private bool NotEmptyArray(Guid[] ids)
    {
        return ids.Count() > 0;
    }
}
