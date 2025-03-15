namespace Domain.Models.User;

public class Role
{
	public int Id { get; init; }
	public string Name { get; init; }

	public ICollection<User> Users { get; init; } = [];
	public ICollection<Permission> Permissions { get; init; } = [];
}