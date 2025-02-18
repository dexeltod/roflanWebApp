using Application.Repositories;
using Application.Services;
using Domain.Models;

namespace Infrastructure.Services;

public class UserValidator : IUserValidator
{
	private readonly IPasswordHasher _passwordHasher;
	private readonly IUserRepository _userRepository;

	public UserValidator(IUserRepository userRepository, IPasswordHasher passwordHasher)
	{
		_userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
		_passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
	}

	public async Task<bool> IsEmailExists(string email, CancellationToken cancellationToken) =>
		await _userRepository.IsEmailExists(email, cancellationToken);

	public bool CheckPassword(string password, string hashedPassword) => _passwordHasher.VerifyPassword(password, hashedPassword);

	public async Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken) =>
		await _userRepository.GetUserByEmail(email, cancellationToken);
}
