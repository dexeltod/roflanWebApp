using Domain.TestModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Utils.ConfigurationModels;

namespace Infrastructure.Contexts;

public class TestContext : DbContext
{
	private const string? BootAssemblyName = "Boot";
	private readonly IOptions<AuthorizationOptions> _authorizationOptions;
	private readonly IConfiguration _configuration;

	public TestContext(
		DbContextOptions<ApplicationContext> options,
		IConfiguration configuration,
		IOptions<AuthorizationOptions> authorizationOptions
	)
	{
		_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		_authorizationOptions = authorizationOptions;
	}

	public DbSet<TestItem> TestItems { get; init; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<TestItem>().HasKey(t => t.Id);
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseNpgsql(
			_configuration.GetConnectionString("DefaultConnection"),
			b => b.MigrationsAssembly(BootAssemblyName)
		);

		base.OnConfiguring(optionsBuilder);
	}
}
