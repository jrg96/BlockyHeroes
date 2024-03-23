using BlockyHeroesBackend.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlockyHeroesBackend.Infrastructure.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _config;

    public JwtTokenService(IConfiguration config)
    {
        _config = config;
    }

    public IEnumerable<Claim> DecodeToken(string token)
    {
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        JwtSecurityToken jwtSecurityToken = handler.ReadJwtToken(token);

        return jwtSecurityToken.Claims;
    }

    public string GenerateToken(Dictionary<string, string> claims)
    {
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        List<Claim> securityClaims = new List<Claim>();

        foreach (KeyValuePair<string, string> entry in claims)
        {
            securityClaims.Add(new Claim(entry.Key, entry.Value));
        }

        var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
          _config["Jwt:Issuer"],
          securityClaims,
          expires: DateTime.Now.AddMinutes(120),
          signingCredentials: credentials);

        var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);
        return token;
    }
}
