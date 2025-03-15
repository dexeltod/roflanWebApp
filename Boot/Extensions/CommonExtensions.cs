using Utils.ConfigurationModels;

namespace Boot.Extensions;

public static class CommonExtensions
{
	public static IServiceCollection ConfigureServices(this IServiceCollection builder, IConfiguration configuration) =>
		builder.Configure<ServicesOptions>(configuration.GetSection(nameof(ServicesOptions)));
}