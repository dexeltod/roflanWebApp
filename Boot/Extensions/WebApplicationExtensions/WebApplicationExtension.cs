using Application.Trash;
using Grpc.Net.Client;
using ProductService;
using static System.Threading.Tasks.Task;
using static Microsoft.AspNetCore.Http.Results;

namespace Boot.Extensions.WebApplicationExtensions;

public static class WebApplicationExtension
{
	public static WebApplication MapMinimalApi(this WebApplication app)
	{
		app.SetKafka();
		app.SetProduct();
		app.SetTest();

		return app;
	}

	private static void SetKafka(this WebApplication app)
	{
		app.MapPost(
			"kafka_test",
			async (KafkaProducer producer, CancellationToken token) =>
			{
				await producer.SendMessageAsync("testTopic", "zdarovaMessage", token);
			}
		);
	}

	private static void SetProduct(this WebApplication app)
	{
		app.MapGet(
			"/set_product",
			async (CancellationToken token) =>
			{
				using GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:5198");

				var client = new ProductService.ProductService.ProductServiceClient(channel);

				var request = new ProductRequest { Id = "123" };
				ProductReply? reply = await client.GetProductAsync(request, cancellationToken: token);

				Console.WriteLine("SetProduct: " + reply.Product);

				return Ok(reply);
			}
		);
	}

	private static void SetTest(this WebApplication app)
	{
		var result = new { name = "resultName" };

		app.MapGet("ok-string", async (CancellationToken _) => await FromResult(result));
	}
}
