using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Application.RabbitMQ;

public class RabbitMqService
{
	private const string QueueName = "hello";

	public async Task SendMessage(string payload)
	{
		var factory = new ConnectionFactory { HostName = "localhost" };
		await using IConnection connection = await factory.CreateConnectionAsync();
		await using IChannel channel = await connection.CreateChannelAsync();

		await channel.QueueDeclareAsync(
			QueueName,
			false,
			false,
			false
		);

		byte[] body = Encoding.UTF8.GetBytes(payload);

		await channel.BasicPublishAsync(string.Empty, QueueName, body);

		Console.WriteLine($" [x] '{payload}' sent");
	}

	public async Task StartReceivingAsync(CancellationToken cancellationToken)
	{
		var factory = new ConnectionFactory { HostName = "localhost" };

		await using IConnection connection = await factory.CreateConnectionAsync(cancellationToken);
		await using IChannel channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

		await channel.QueueDeclareAsync(QueueName, false, false, false, cancellationToken: cancellationToken);

		Console.WriteLine(" [*] Waiting for messages.");

		var consumer = new AsyncEventingBasicConsumer(channel);

		consumer.ReceivedAsync += async (model, eventArgs) =>
		{
			try
			{
				byte[] body = eventArgs.Body.ToArray();
				string message = Encoding.UTF8.GetString(body);

				Console.WriteLine($" [x] Received: {message}");

				await Task.Yield(); // To avoid blocking the dispatcher thread
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error processing message {ex}");
			}
		};

		await channel.BasicConsumeAsync(QueueName, true, consumer, cancellationToken);
		while (!cancellationToken.IsCancellationRequested) await Task.Delay(TimeSpan.FromMilliseconds(100), cancellationToken);
		Console.WriteLine("Stopped Receiving");
	}
}
