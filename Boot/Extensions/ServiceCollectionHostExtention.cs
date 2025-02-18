using Application.Implementations.Services;

namespace Boot.Extensions;

public static class ServiceCollectionHostExtention
{
	public static IServiceCollection AddHostedServices(this IServiceCollection services)
	{
		services.AddHostedService<RabbitMqBackgroundService>();
		return services;
	}
}
