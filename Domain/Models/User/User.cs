namespace Domain.Models.User;

public class User
{
	public long Id { get; init; }
	public string Name { get; init; }
	public string PasswordHash { get; init; }
	public string Email { get; init; }
	public DateTime CreatedAt { get; init; }
	public int Age { get; init; }
	public ICollection<Post> Posts { get; set; } = [];
	public ICollection<Role> Roles { get; set; } = [];
}