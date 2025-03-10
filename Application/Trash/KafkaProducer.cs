using Confluent.Kafka;
using Microsoft.Extensions.Hosting;

namespace Application.Trash;

public class KafkaProducer : IHostedService
{
	private readonly IProducer<string, string> _producer;

	public KafkaProducer()
	{
		var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

		_producer = new ProducerBuilder<string, string>(config).Build();
	}

	public async Task StartAsync(CancellationToken cancellationToken)
	{
		var topic = "testTopic";
		var message = "djaaosjdoasj";

		DeliveryResult<string, string>? deliveryResult = await _producer.ProduceAsync(
			topic,
			new Message<string, string> { Key = null, Value = message },
			cancellationToken
		);

		// producer.Flush(cancellationToken);
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		_producer.Dispose();
		return Task.CompletedTask;
	}

	public async Task SendMessageAsync(string topic, string message, CancellationToken cancellationToken)
	{
	}
}
