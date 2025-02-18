using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ModelConfigurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(
            ur => new
            {
                ur.UserId,
                ur.RoleId
            }
        );

        builder.HasOne<User>().WithMany().HasForeignKey(ur => ur.UserId);
        builder.HasOne<Role>().WithMany().HasForeignKey(ur => ur.RoleId);
    }
}
