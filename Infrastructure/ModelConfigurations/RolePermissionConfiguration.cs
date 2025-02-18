using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utils.ConfigurationModels;
using Utils.Enums;

namespace Infrastructure.ModelConfigurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
	private readonly AuthorizationOptions _authorizationOptions;

	public RolePermissionConfiguration(AuthorizationOptions authorizationOptions) =>
		_authorizationOptions = authorizationOptions ?? throw new ArgumentNullException(nameof(authorizationOptions));

	public void Configure(EntityTypeBuilder<RolePermission> builder)
	{
		builder.HasKey(
			rp => new { rp.RoleId, rp.PermissionId }
		);

		builder.HasData(GetRolePermissionEntities());
	}

	private IEnumerable<RolePermission> GetRolePermissionEntities()
	{
		return _authorizationOptions.RolePermissions
			.SelectMany(
				rp => rp.Permissions
					.Select(
						p => new RolePermission
						{
							RoleId = (int)Enum.Parse<RoleEnum>(rp.Role), PermissionId = (int)Enum.Parse<PermissionEnum>(p)
						}
					)
			);
	}
}
