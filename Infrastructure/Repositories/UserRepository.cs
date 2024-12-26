using Application.DTO;
using Application.Repositories;
using Domain.Models;
using Infrastructure.Contexts;
using Infrastructure.Factories;
using Microsoft.EntityFrameworkCore;
using Utils.Exceptions;

namespace Infrastructure.Repositories;

public sealed class UserRepository(ApplicationContext applicationContext, UserFactory userFactory) : IUserRepository
{
    private readonly ApplicationContext _applicationContext =
        applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));

    private readonly UserFactory _userFactory = userFactory ?? throw new ArgumentNullException(nameof(userFactory));

    public async Task AddUser(UserDataTransferObject userData)
    {
        var user = _userFactory.Create(userData.Name, userData.Age, userData.Email, userData.Password);

        NullValidator.ValidateNull(user);

        _applicationContext.Users.Add(user);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task DeleteUser(int id, Action? onNull = null)
    {
        NullValidator.ValidateNull(id);

        User? user = await GetUserById(id);

        if (user != null) _applicationContext.Users.Remove(user);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task UpdateUser(int id)
    {
        User? user = await GetUserById(id);
        if (user == null) throw new ArgumentNullException(nameof(user));

        NullValidator.ValidateNull(user);

        _applicationContext.Users.Update(user);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task<List<User?>> GetUsers(int[] ids, Action onNull = null)
    {
        NullValidator.ValidateNull(ids);

        List<User?> users = [];

        foreach (var id in ids) users.Add(await GetUserById(id, onNull));

        return users;
    }

    public async Task<User?> GetUserById(int id, Action? onNull = null)
    {
        User? result = await _applicationContext.Users.FindAsync(id);

        if (result == null) onNull!.Invoke();

        return result;
    }

    public async Task Register(string name, int age, string password, string email)
    {
    }

    public async Task<bool> EmailExists(string email, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(email))
            throw new ArgumentException($"Email {email} cannot be null or empty.", nameof(email));

        return await _applicationContext.Users.AnyAsync(u => u.Email == email, cancellationToken);
    }
}