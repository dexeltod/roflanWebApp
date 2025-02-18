using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Factories;

public interface IJwtTokenFactory
{
    JwtSecurityToken Create(IEnumerable<Claim> claims, int expirationTime = 0);
}