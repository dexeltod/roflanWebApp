using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication;

public class AuthOptions
{
	public AuthOptions(IConfiguration configuration)
	{
		Key = configuration["AuthOptions:JwtSecretKey"]
		      ?? throw new InvalidOperationException("AuthOptions:JwtSecretKey not found");

		Audience = configuration["AuthOptions:Audience"] ?? throw new InvalidOperationException("AuthOptions:Audience not found");
		Issuer = configuration["AuthOptions:Issuer"] ?? throw new InvalidOperationException("AuthOptions:Issuer not found");
	}

	public string Audience { get; }
	public string Issuer { get; }

	private string Key { get; }

	public SymmetricSecurityKey GetSymmetricSecurityKey() => new(Encoding.UTF8.GetBytes(Key));
}