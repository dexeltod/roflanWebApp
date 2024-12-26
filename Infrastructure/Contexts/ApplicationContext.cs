using Domain.Models;
using Infrastructure.ModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Contexts;

public sealed class ApplicationContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options, IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            _configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly("Presentation")
        );

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PostEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
    }
}