using Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utils.Enums;

namespace Infrastructure.ModelConfigurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
	public void Configure(EntityTypeBuilder<Role> builder)
	{
		builder.HasKey(r => r.Id);
		builder.Property(r => r.Name).IsRequired();

		builder
			.HasMany(r => r.Permissions)
			.WithMany(p => p.Roles)
			.UsingEntity<RolePermission>(
				l => l.HasOne<Permission>().WithMany().HasForeignKey(e => e.PermissionId),
				r => r.HasOne<Role>().WithMany().HasForeignKey(e => e.RoleId)
			);

		builder.HasData(GetRoles());
	}

	private IEnumerable<Role> GetRoles()
	{
		return Enum
			.GetValues<RoleEnum>()
			.Select(
				role => new Role { Id = (int)role, Name = role.ToString() }
			);
	}
}