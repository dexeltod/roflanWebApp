using Application.DTO;
using Application.Repositories;
using Application.Services;
using Domain.Models;
using Infrastructure.Contexts;
using Infrastructure.Factories;
using Microsoft.EntityFrameworkCore;
using Utils.Enums;
using Utils.Exceptions;

namespace Infrastructure.Repositories;

public sealed class UserRepository(
    ApplicationContext applicationContext,
    UserFactory userFactory,
    IPasswordHasher passwordHasher
) :
    IUserRepository
{
    private const int ExpirationTime = 2;

    private readonly ApplicationContext _applicationContext =
        applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));

    private readonly IPasswordHasher _passwordHasher =
        passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));

    private readonly UserFactory _userFactory = userFactory ?? throw new ArgumentNullException(nameof(userFactory));

    public async Task<List<User?>> GetUsers(long[] ids, CancellationToken cancellationToken)
    {
        NullValidator.ValidateNull(ids);

        List<User?> users = [];

        foreach (int id in ids) users.Add(await GetUserById(id, cancellationToken));

        return users;
    }

    public async Task<bool> IsEmailExists(string email, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(email))
            throw new ArgumentException($"Email {email} cannot be null or empty.", nameof(email));

        bool any = await _applicationContext.Users.AnyAsync(u => u.Email == email, cancellationToken);

        return any;
    }

    public async Task Register(UserDataTransferObject userData, CancellationToken cancellationToken)
    {
        string hashedPassword = _passwordHasher.HashPassword(userData.Password);

        Role role = await _applicationContext.Roles
                        .SingleOrDefaultAsync(e => e.Id == (int)RoleEnum.User, cancellationToken) ??
                    throw new InvalidOperationException();

        User user =
            _userFactory.Create(
                userData.Name,
                userData.Age,
                userData.Email,
                hashedPassword,
                role
            );

        await _applicationContext.Users.AddAsync(user, cancellationToken);
        await _applicationContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken) =>
        await _applicationContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

    public async Task UpdateUser(long id, CancellationToken cancellationToken)
    {
        User? user = await GetUserById(id, cancellationToken);
        if (user == null) throw new ArgumentNullException(nameof(user));

        NullValidator.ValidateNull(user);

        _applicationContext.Users.Update(user);
        await _applicationContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteUser(long id, CancellationToken cancellationToken)
    {
        NullValidator.ValidateNull(id);

        User? user = await GetUserById(id, cancellationToken);

        if (user != null) _applicationContext.Users.Remove(user);
        await _applicationContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<User?> GetUserById(long id, CancellationToken cancellationToken)
    {
        User? a = await _applicationContext.Users
            .Include(u => u.Roles)
            .ThenInclude(r => r.Permissions)
            .FirstAsync(
                u => u.Id == id,
                cancellationToken
            );

        return a;
    }
}
