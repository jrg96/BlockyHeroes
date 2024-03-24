using BlockyHeroesBackend.Application.Abstractions;
using BlockyHeroesBackend.Application.Common;
using BlockyHeroesBackend.Application.Entities.User.Commands;
using BlockyHeroesBackend.Application.Services;
using BlockyHeroesBackend.Domain.Common.ValueObjects.Common;
using BlockyHeroesBackend.Domain.Common.ValueObjects.User;
using BlockyHeroesBackend.Domain.Repositories;
using BlockyHeroesBackend.Domain.Repositories.Query;

namespace BlockyHeroesBackend.Application.Entities.User.CommandHandlers;

public class LoginUserCommandHandler : IOperationHandler<LoginUserCommand, Domain.Entities.User.User>
{
    private readonly IUserSecurityService _userSecurityService;
    private readonly IUserQueryRepository _userQueryRepository;

    public LoginUserCommandHandler(IUserQueryRepository userQueryRepository, IUserSecurityService userSecurityService)
    {
        _userQueryRepository = userQueryRepository;
        _userSecurityService = userSecurityService;
    }

    public async Task<OperationResult<Domain.Entities.User.User>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        // Check if user exists
        Domain.Entities.User.User? user = await _userQueryRepository.GetByIdAsync(new UserId(request.Id));
        bool valid = user != null ? 
            _userSecurityService.VerifyPassword(request.Password, user.Password, user.Salt)
            : false;

        if (user == null || !valid) 
        {
            return new OperationResult<Domain.Entities.User.User>()
            {
                Success = false,
                Errors = new List<Error>() { new Error(1, "Invalid username or password") }
            };
        }

        return new OperationResult<Domain.Entities.User.User>()
        {
            Success = true,
            Data = user
        };
    }
}
