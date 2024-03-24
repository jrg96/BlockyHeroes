using BlockyHeroesBackend.Application.Abstractions;

namespace BlockyHeroesBackend.Application.Entities.User.Commands;

public sealed record LoginUserCommand(
    Guid Id,
    string Password
) : IOperation<Domain.Entities.User.User>;
