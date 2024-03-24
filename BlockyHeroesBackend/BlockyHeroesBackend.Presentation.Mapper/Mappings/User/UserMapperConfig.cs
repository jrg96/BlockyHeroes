using BlockyHeroesBackend.Presentation.RequestResponse.User.Dto;
using Mapster;

namespace BlockyHeroesBackend.Presentation.Mapper.Mappings.User;

public class UserMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<Domain.Entities.User, UserDto>()
            .Map(dest => dest.Id, src => src.Id.Value);
    }
}
