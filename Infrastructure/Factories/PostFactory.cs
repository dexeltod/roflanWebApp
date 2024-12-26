using Domain.Models;
using Infrastructure.Contexts;

namespace Infrastructure.Factories;

public class PostFactory
{
    private readonly ApplicationContext _applicationContext;

    public PostFactory(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
    }

    public async Task<Post> Create(long authorId, string title, string content)
    {
        var userFromBd = await _applicationContext.Users.FindAsync(authorId);
        if (userFromBd == null) throw new ArgumentNullException(nameof(userFromBd));

        return new Post(title, content, userFromBd);
    }
}