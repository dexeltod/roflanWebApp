using Application.DTO;
using Domain.Models.User;

namespace Application.Repositories;

public interface IUserRepository
{
	Task DeleteUser(long id, CancellationToken cancellationToken);
	Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken);
	Task<User?> GetUserById(long id, CancellationToken cancellationToken);
	Task<List<User?>> GetUsers(long[] ids, CancellationToken cancellationToken);
	Task<bool> IsEmailExists(string email, CancellationToken cancellationToken);
	Task Register(UserDataTransferObject userData, CancellationToken cancellationToken);
	Task UpdateUser(long id, CancellationToken cancellationToken);
}