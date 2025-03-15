using Domain.Models;
using Domain.Models.User;
using Infrastructure.ModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Utils.ConfigurationModels;

namespace Infrastructure.Contexts;

public sealed class ApplicationContext : DbContext
{
	private const string? BootAssemblyName = "Boot";
	private readonly IOptions<AuthorizationOptions> _authorizationOptions;
	private readonly IConfiguration _configuration;

	public ApplicationContext(
		DbContextOptions<ApplicationContext> options,
		IConfiguration configuration,
		IOptions<AuthorizationOptions> authorizationOptions
	)
	{
		_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		_authorizationOptions = authorizationOptions;
	}

	public DbSet<User> Users { get; init; }
	public DbSet<Role> Roles { get; init; }
	public DbSet<UserRole> UserRole { get; init; }
	public DbSet<Post> Posts { get; init; }
	public DbSet<RolePermission> RolePermissions { get; init; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseNpgsql(
			_configuration.GetConnectionString("DefaultConnection"),
			b => b.MigrationsAssembly(BootAssemblyName)
		);

		base.OnConfiguring(optionsBuilder);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new PermissionConfiguration());
		modelBuilder.ApplyConfiguration(new RolePermissionConfiguration(_authorizationOptions.Value));
		modelBuilder.ApplyConfiguration(new PostEntityConfiguration());
		modelBuilder.ApplyConfiguration(new UserConfiguration());
		modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
		modelBuilder.ApplyConfiguration(new RoleConfiguration());
	}
}