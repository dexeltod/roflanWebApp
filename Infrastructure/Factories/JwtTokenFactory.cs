using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Factories;
using Infrastructure.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Factories;

public class JwtTokenFactory : IJwtTokenFactory
{
	private readonly JwtTokenValidationParamsFactory _jwtJwtTokenParamsFactory;

	public JwtTokenFactory(JwtTokenValidationParamsFactory jwtJwtTokenParamsFactory) =>
		_jwtJwtTokenParamsFactory = jwtJwtTokenParamsFactory ?? throw new ArgumentNullException(nameof(jwtJwtTokenParamsFactory));

	public JwtSecurityToken Create(IEnumerable<Claim> claims, int expirationTime = 0)
	{
		TokenValidationParameters tokenValidationParameters = _jwtJwtTokenParamsFactory.Create();

		SecurityTokenDescriptor securityTokenDescriptor = CreateDescriptor(claims, expirationTime, tokenValidationParameters);

		return new JwtSecurityTokenHandler().CreateJwtSecurityToken(securityTokenDescriptor);
	}

	private SecurityTokenDescriptor CreateDescriptor(
		IEnumerable<Claim> claims,
		int expirationHours,
		TokenValidationParameters parameters) =>
		new()
		{
			Issuer = parameters.ValidIssuer,
			Audience = parameters.ValidAudience,
			Subject = new ClaimsIdentity(claims),
			Expires = expirationHours > 0 ? DateTime.UtcNow.AddHours(expirationHours) : null,
			SigningCredentials = new SigningCredentials(
				parameters.IssuerSigningKey,
				SecurityAlgorithms.HmacSha256Signature
			)
		};
}