using BlockyHeroesBackend.Application.Abstractions;

namespace BlockyHeroesBackend.Application.Entities.User.Commands;

public sealed record CreateUserCommand() 
    : IOperation<Domain.Entities.User>;
