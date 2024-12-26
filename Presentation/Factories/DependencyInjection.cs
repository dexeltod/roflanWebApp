using System.Reflection;
using Application.Repositories;
using Application.Services;
using Infrastructure.Contexts;
using Infrastructure.Factories;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.Validation;
using Microsoft.Extensions.Localization;

[assembly: RootNamespace("Presentation")]

namespace Presentation.Factories;

public class DependencyInjection
{
    private readonly IServiceCollection _serviceCollection;

    public DependencyInjection(IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));
    }

    public void AddServices()
    {
        _serviceCollection.AddLocalization();

        _serviceCollection.AddSingleton<UserValidator>();
        _serviceCollection.AddSingleton<PostValidator>();
        _serviceCollection.AddSingleton<IUserService, UserService>();

        _serviceCollection.AddScoped<UserFactory>();

        _serviceCollection.AddScoped<PostFactory>();
        _serviceCollection.AddScoped<IPostsRepository, PostsRepository>();
        _serviceCollection.AddScoped<IUserRepository, UserRepository>();

        _serviceCollection.AddDbContext<ApplicationContext>();
    }
}