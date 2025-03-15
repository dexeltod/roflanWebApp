using Domain.Models.User;

namespace Application.Services;

public interface IUserValidator
{
	bool CheckPassword(string password, string hashedPassword);
	Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken);
	Task<bool> IsEmailExists(string email, CancellationToken cancellationToken);
}