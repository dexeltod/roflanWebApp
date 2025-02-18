using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication;

public class JwtTokenValidationParamsFactory
{
    private readonly AuthOptions _authOptions;

    public JwtTokenValidationParamsFactory(AuthOptions authOptions) =>
        _authOptions = authOptions ?? throw new ArgumentNullException(nameof(authOptions));

    public TokenValidationParameters Create() =>
        new()
        {
            ValidIssuer = _authOptions.Issuer,
            ValidAudience = _authOptions.Audience,
            IssuerSigningKey = _authOptions.GetSymmetricSecurityKey(),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
}
