using Application.Factories;
using Infrastructure.Authentication;
using Infrastructure.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Utils.ConfigurationModels;

namespace Boot.Extensions.BuilderExtensions;

public static class ServiceCollectionAuthExtension
{
	public static IServiceCollection AddAuthOptions(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<AuthorizationOptions>(configuration.GetSection(nameof(AuthorizationOptions)));

		return services;
	}

	public static IServiceCollection AddAuthServices(this IServiceCollection services)
	{
		services.AddScoped<JwtTokenValidationParamsFactory>();
		services.AddScoped<IJwtTokenFactory, JwtTokenFactory>();

		services.AddScoped<AuthOptions>();

		JwtTokenValidationParamsFactory factory =
			services.BuildServiceProvider().GetService<JwtTokenValidationParamsFactory>()
			?? throw new ArgumentNullException($"{nameof(JwtTokenValidationParamsFactory)} is null");

		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
			options => { options.TokenValidationParameters = factory.Create(); }
		);

		services.AddAuthorizationBuilder()
			.AddPolicy("Admin", policy => policy.RequireClaim("Role", "Admin"))
			.AddPolicy("User", policy => policy.RequireClaim("Role", "User"));
		return services;
	}
}