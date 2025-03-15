namespace Domain.Models.User;

public class Permission
{
	public int Id { get; init; }
	public string Name { get; init; } = null!;

	public ICollection<Role> Roles { get; init; } = [];
}