using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Application.Trash;

public class KafkaConsumer : IHostedService
{
	private readonly IConsumer<Ignore, string> _consumer;
	private readonly ILogger<KafkaConsumer> _logger;

	public KafkaConsumer(ILogger<KafkaConsumer> logger)
	{
		_logger = logger;
		var config = new ConsumerConfig
		{
			BootstrapServers = "localhost:9092", GroupId = "test-group", AutoOffsetReset = AutoOffsetReset.Earliest
		};
		_consumer = new ConsumerBuilder<Ignore, string>(config).Build();
	}

	public Task StartAsync(CancellationToken cancellationToken)
	{
		_consumer.Subscribe("testTopic");

		Task.Run(
			() =>
			{
				while (!cancellationToken.IsCancellationRequested)
					try
					{
						ConsumeResult<Ignore, string>? consumeResult = _consumer.Consume(cancellationToken);
						_logger.LogInformation($"Received message: {consumeResult.Message.Value}");
					}
					catch (OperationCanceledException)
					{
						break;
					}
					catch (ConsumeException e)
					{
						_logger.LogError($"Error occurred: {e.Error.Reason}");
					}
			},
			cancellationToken
		);

		return Task.CompletedTask;
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		_consumer.Close();
		return Task.CompletedTask;
	}
}
