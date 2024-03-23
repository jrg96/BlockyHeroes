using System.Security.Claims;

namespace BlockyHeroesBackend.Application.Services;

public interface IJwtTokenService
{
    string GenerateToken(Dictionary<string, string> claims);
    IEnumerable<Claim> DecodeToken(string token);
}
