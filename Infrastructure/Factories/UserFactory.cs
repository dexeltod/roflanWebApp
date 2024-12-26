using Domain.Models;

namespace Infrastructure.Factories;

public class UserFactory
{
    public User? Create(string name, int age, string email, string passwordHash)
    {
        return new User
        {
            Name = name,
            Age = age
        };
    }
}