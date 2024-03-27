using BlockyHeroesBackend.Application.Entities.UserCharacter.Commands;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using BlockyHeroesBackend.Domain.Entities.User;
using FluentValidation;

namespace BlockyHeroesBackend.Application.Entities.UserCharacter.Validators;

public class UpgradeUserCharacterCommandValidator : AbstractValidator<UpgradeUserCharacterCommand>
{
    public UpgradeUserCharacterCommandValidator()
    {
        RuleFor(userCharacter => userCharacter.UserId)
            .NotNull();

        RuleFor(userCharacter => userCharacter.UserCharacterId)
            .NotNull();

        RuleFor(userCharacter => userCharacter.LevelsToUpgrade)
            .GreaterThanOrEqualTo(1);
    }
}
