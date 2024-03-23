using BlockyHeroesBackend.Application.Services;
using System.Security.Cryptography;
using System.Text;

namespace BlockyHeroesBackend.Infrastructure.Services;

public class UserSecurityService : IUserSecurityService
{
    private readonly int _keySize = 64;
    private readonly int _iterations = 350000;
    private readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;

    public (byte[] salt, string hash) HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(_keySize);

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            _iterations,
            _hashAlgorithm,
            _keySize);

        var hashedPassword = Convert.ToHexString(hash);

        return (salt, hashedPassword);
    }

    public bool VerifyPassword(string password, string hash, byte[] salt)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, _iterations, _hashAlgorithm, _keySize);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
    }
}
