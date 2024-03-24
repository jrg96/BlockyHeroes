using BlockyHeroesBackend.Application.Entities.User.Commands;
using FluentValidation;

namespace BlockyHeroesBackend.Application.Entities.User.Validators;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotNull();

        RuleFor(command => command.Password)
            .NotNull()
            .NotEmpty();
    }
}
