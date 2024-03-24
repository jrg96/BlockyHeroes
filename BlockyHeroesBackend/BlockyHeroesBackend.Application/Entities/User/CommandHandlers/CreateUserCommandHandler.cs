using BlockyHeroesBackend.Application.Abstractions;
using BlockyHeroesBackend.Application.Common;
using BlockyHeroesBackend.Application.Entities.User.Commands;
using BlockyHeroesBackend.Application.Services;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Common;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using BlockyHeroesBackend.Domain.Repositories;
using BlockyHeroesBackend.Domain.Repositories.Command;

namespace BlockyHeroesBackend.Application.Entities.User.CommandHandlers;

public class CreateUserCommandHandler : IOperationHandler<CreateUserCommand, Domain.Entities.User>
{
    private readonly IUserCommandRepository _userCommandRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSecurityService _userSecurityService;

    public CreateUserCommandHandler(IUserCommandRepository userCommandRepository, IUnitOfWork unitOfWork, IUserSecurityService userSecurityService)
    {
        _userCommandRepository = userCommandRepository;
        _unitOfWork = unitOfWork;
        _userSecurityService = userSecurityService;
    }

    public async Task<OperationResult<Domain.Entities.User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // By default when creating new account, populate it with default data
        UserId userId = UserId.CreateUserId();

        // To Create the final users password, will generate a random sequence of characters
        // Through encryption, and later that password will be rehashed to get the final encrypted password
        (byte[] salt, string hash) = _userSecurityService.HashPassword(userId.Value.ToString());
        (byte[] saltFinal, string hashFinal) = _userSecurityService.HashPassword(hash);

        Domain.Entities.User user = new Domain.Entities.User()
        {
            Id = userId,
            Name = "Player",
            Role = Roles.User,
            Password = hashFinal,
            Salt = saltFinal,
            Email = $"{userId.Value.ToString()}@blockyheroes.com"
        };

        await _userCommandRepository.InsertAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new OperationResult<Domain.Entities.User>()
        {
            Success = true,
            Errors = new List<Error>(),
            Data = new Domain.Entities.User()
            {
                Id = user.Id,
                Name = user.Name,
                Role = Roles.User,
                Password = hash,
                Email = user.Email,
            }
        };
    }
}
