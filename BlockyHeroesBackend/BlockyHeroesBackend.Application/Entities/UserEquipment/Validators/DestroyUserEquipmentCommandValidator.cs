using BlockyHeroesBackend.Application.Entities.UserEquipment.Commands;
using FluentValidation;

namespace BlockyHeroesBackend.Application.Entities.UserEquipment.Validators;

public class DestroyUserEquipmentCommandValidator : AbstractValidator<DestroyUserEquipmentCommand>
{
    public DestroyUserEquipmentCommandValidator()
    {
        RuleFor(userEquip => userEquip.UserId)
            .NotNull();

        RuleFor(userEquip => userEquip.UserEquipmentId)
            .NotNull();
    }
}
