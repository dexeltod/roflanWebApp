using Application.DTO;
using Domain.Models;

namespace Application.Repositories;

public interface IUserRepository
{
    Task AddUser(UserDataTransferObject userData);
    Task DeleteUser(int id, Action? onNull);
    Task UpdateUser(int id);
    Task<List<User?>> GetUsers(int[] ids, Action? onNull);
    Task<User?> GetUserById(int id, Action? onNull);
    Task Register(string name, int age, string password, string email);
    Task<bool> EmailExists(string email, CancellationToken cancellationToken);
}