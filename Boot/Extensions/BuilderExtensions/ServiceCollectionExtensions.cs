using Application.RabbitMQ;
using Application.Repositories;
using Application.Services;
using Application.Validation;
using Infrastructure;
using Infrastructure.Contexts;
using Infrastructure.Factories;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.Validation;

namespace Boot.Extensions.BuilderExtensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddContexts(this IServiceCollection services)
	{
		services.AddDbContext<ApplicationContext>();
		services.AddDbContext<TestContext>();

		return services;
	}

	public static IServiceCollection AddPostServices(this IServiceCollection services)
	{
		services.AddScoped<PostValidator>();
		return services;
	}

	public static IServiceCollection AddRabbitMq(this IServiceCollection services)
	{
		services.AddSingleton<RabbitMqService>();

		return services;
	}

	public static IServiceCollection AddUserServices(this IServiceCollection services)
	{
		services.AddScoped<UserFactory>();
		services.AddScoped<IUserRepository, UserRepository>();
		services.AddScoped<IUserValidator, UserValidator>();
		services.AddScoped<IPasswordHasher, PasswordHasher>();

		services.AddScoped<IUserRegistrationValidator, UserRegistrationValidator>();
		services.AddScoped<IUserLoginValidator, UserLoginValidator>();
		services.AddScoped<IUserValidatorById, UserValidatorById>();
		return services;
	}

	public static IServiceCollection RegisterControllers(this IServiceCollection services)
	{
		services.AddControllers();
		return services;
	}
}