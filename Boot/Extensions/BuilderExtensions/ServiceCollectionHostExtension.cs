using Application.Implementations.Services;
using Application.Trash;

namespace Boot.Extensions.BuilderExtensions;

public static class ServiceCollectionHostExtension
{
	public static IServiceCollection AddHostedServices(this IServiceCollection services)
	{
		services.AddHostedService<RabbitMqBackgroundService>();
		return services;
	}

	public static IServiceCollection AddKafka(this IServiceCollection services)
	{
		services.AddHostedService<KafkaProducer>();
		services.AddHostedService<KafkaConsumer>();

		return services;
	}
}
