using Domain.Models;

namespace Infrastructure.Factories;

public class UserFactory
{
    public User Create(string name, int age, string email, string passwordHash, Role role)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

        if (string.IsNullOrEmpty(email)) throw new ArgumentException("Value cannot be null or empty.", nameof(email));

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(passwordHash));

        ArgumentOutOfRangeException.ThrowIfNegative(age);

        var newUser = new User
        {
            Name = name,
            Age = age,
            Email = email,
            PasswordHash = passwordHash,
            Roles = [role]
        };

        return newUser;
    }
}
