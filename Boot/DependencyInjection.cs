using Boot.Extensions;

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
			.RegisterControllers()
			.AddAuthOptions(_configuration)
			.AddAuthServices()
			.AddEndpointsApiExplorer()
			.AddSwaggerGen()
			.AddLocalization()
			.AddUserServices()
			.AddPostServices()
			.AddContexts();
		// .AddRabbitMq()
		// .AddHostedServices();

		AddGrpcServices();
	}

	private void AddGrpcServices()
	{
		// _serviceCollection.AddGrpcClient<>();
	}
}
