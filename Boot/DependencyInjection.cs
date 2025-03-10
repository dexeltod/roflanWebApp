using Boot.Extensions;
using Boot.Extensions.BuilderExtensions;

namespace Boot;

public class DependencyInjection
{
	private readonly IConfiguration _configuration;

	private readonly IServiceCollection _serviceCollection;

	public DependencyInjection(IServiceCollection serviceCollection, IConfiguration configuration)
	{
		_serviceCollection =
			serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));
		_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
	}

	public void AddServices()
	{
		_serviceCollection
			.ConfigureServices(_configuration)
			.RegisterControllers()
			.AddAuthOptions(_configuration)
			.AddAuthServices()
			.AddEndpointsApiExplorer()
			.AddSwaggerGen()
			.AddLocalization()
			.AddUserServices()
			.AddPostServices()
			.AddContexts()
			.AddKafka()
			.AddCors(
				options =>
				{
					options.AddPolicy(
						"AllowReactApp",
						policy =>
						{
							policy.WithOrigins("http://localhost:3000") // Разрешить запросы с этого домена
								.AllowAnyHeader() // Разрешить любые заголовки
								.AllowAnyMethod(); // Разрешить любые HTTP-методы (GET, POST и т.д.)
						}
					);
				}
			);
		// .AddRabbitMq()
		// .AddHostedServices();

		AddGrpcServices();
	}

	private void AddGrpcServices()
	{
		// _serviceCollection.AddGrpcClient<>();
	}
}
