using Application.Services;

namespace Infrastructure;

public class PasswordHasher : IPasswordHasher
{
	private const int WorkFactor = 12;

	public string HashPassword(string password)
	{
		string? salt = BCrypt.Net.BCrypt.GenerateSalt(WorkFactor);
		return BCrypt.Net.BCrypt.HashPassword(password, salt);
	}

	public bool VerifyPassword(string password, string hashedPassword) => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}