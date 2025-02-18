using Confluent.Kafka;

namespace Application.Trash;

public class KafkaTest
{
	private readonly ProducerConfig _config;

	public KafkaTest() => _config = new ProducerConfig { BootstrapServers = "localhost:9092" };

	public async Task SendMessageAsync(string topic, string message)
	{
		using IProducer<string, string>? producer = new ProducerBuilder<string, string>(_config).Build();

		await producer.ProduceAsync(topic, new Message<string, string> { Key = null, Value = message });

		producer.Flush();
	}
}
