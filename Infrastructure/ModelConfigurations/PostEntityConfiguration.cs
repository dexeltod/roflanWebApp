using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ModelConfigurations;

public class PostEntityConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Title).IsRequired().HasMaxLength(30);
        builder.Property(p => p.Content).IsRequired().HasMaxLength(700);
        builder.Property(p => p.CreatedAt).HasDefaultValueSql("now()");
        
        builder.HasOne<User>().WithMany(u => u.Posts).HasForeignKey(p => p.AuthorId);
    }
}