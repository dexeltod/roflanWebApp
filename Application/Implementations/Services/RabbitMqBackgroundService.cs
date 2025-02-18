using Application.RabbitMQ;
using Microsoft.Extensions.Hosting;

namespace Application.Implementations.Services;

public class RabbitMqBackgroundService : BackgroundService
{
	private readonly RabbitMqService _rabbitMqService;

	public RabbitMqBackgroundService(RabbitMqService rabbitMqService) =>
		_rabbitMqService = rabbitMqService ?? throw new ArgumentNullException(nameof(rabbitMqService));

	protected async override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		await _rabbitMqService.StartReceivingAsync(stoppingToken);
	}
}
