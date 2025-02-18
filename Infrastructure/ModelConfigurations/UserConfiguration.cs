using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ModelConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name).IsRequired().HasMaxLength(30);
        builder.Property(u => u.PasswordHash).IsRequired();

        builder.Property(u => u.Email).IsRequired().HasMaxLength(320);

        builder.Property(u => u.Age).IsRequired();

        builder.HasMany(u => u.Posts).WithOne(u => u.Author).HasForeignKey(u => u.AuthorId);

        builder.Property(u => u.CreatedAt).HasDefaultValueSql("now()");

        builder.HasMany(u => u.Roles).WithMany(r => r.Users)
            .UsingEntity<UserRole>(
                r => r.HasOne<Role>().WithMany().HasForeignKey(ur => ur.RoleId),
                u => u.HasOne<User>().WithMany().HasForeignKey(ur => ur.UserId)
            );
    }
}
