using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ModelConfigurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Id).ValueGeneratedOnAdd();
        builder.Property(u => u.Name).IsRequired().HasMaxLength(30);
        builder.Property(u => u.Age).IsRequired().HasMaxLength(180);
        builder.HasMany(u => u.Posts).WithOne(u => u.Author).HasForeignKey(u => u.AuthorId);
        builder.Property(u => u.CreatedAt).HasDefaultValueSql("now()");
    }
}