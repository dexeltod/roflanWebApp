using Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utils.Enums;

namespace Infrastructure.ModelConfigurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
	public void Configure(EntityTypeBuilder<Permission> builder)
	{
		builder.HasKey(p => p.Id);

		builder
			.HasMany(r => r.Roles)
			.WithMany(p => p.Permissions)
			.UsingEntity<RolePermission>(
				l => l.HasOne<Role>().WithMany().HasForeignKey(e => e.RoleId),
				r => r.HasOne<Permission>().WithMany().HasForeignKey(e => e.PermissionId)
			);

		PermissionEnum[] a = Enum.GetValues<PermissionEnum>();

		IEnumerable<Permission> permissions = Enum
			.GetValues<PermissionEnum>()
			.Select(
				permissionEnum =>
					new Permission { Id = (int)permissionEnum, Name = permissionEnum.ToString() }
			);

		builder.HasData(permissions);
	}
}