using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystem.Backend.Domain.Entities;

namespace TaskManagementSystem.Backend.Infrastructure.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => new { u.FirstName, u.LastName, u.Email }).IsUnique();

            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(64);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(64);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(128);

            builder.HasMany(u => u.Projects).WithOne(p => p.CreatorUser).HasForeignKey(p => p.CreatorUserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(u => u.CreatedTasks).WithOne(p => p.CreatorUser).HasForeignKey(p => p.CreatorUserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(u => u.AssignedTasks).WithOne(p => p.AssignedUser).HasForeignKey(p => p.AssignedUserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(u => u.Comments).WithOne(p => p.CommentMaker).HasForeignKey(p => p.CommentMakerId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
