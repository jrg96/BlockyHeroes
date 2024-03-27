using BlockyHeroesBackend.Application.Entities.UserEquipment.Commands;
using FluentValidation;

namespace BlockyHeroesBackend.Application.Entities.UserEquipment.Validators;

public class UpgradeEquipmentCommandValidator : AbstractValidator<UpgradeEquipmentCommand>
{
    public UpgradeEquipmentCommandValidator()
    {
        RuleFor(userEquip => userEquip.UserId)
            .NotNull();

        RuleFor(userEquip => userEquip.EquipLevelId)
            .NotNull();

        RuleFor(userEquip => userEquip.Levels)
            .GreaterThanOrEqualTo(1);
    }
}
