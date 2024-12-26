namespace Domain.Models;

public class User
{
    public User()
    {
    }

    public User(long id)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(id);
        Id = id;
    }

    public long Id { get; init; }
    public string Name { get; init; }
    public string PasswordHash { get; init; }
    public string Email { get; init; }
    public DateTime CreatedAt { get; init; }
    public int Age { get; init; }
    public List<Post> Posts { get; init; }
}