namespace BlockyHeroesBackend.Application.Services;

public interface IUserSecurityService
{
    (byte[] salt, string hash) HashPassword(string password);
    bool VerifyPassword(string password, string hash, byte[] salt);
}
