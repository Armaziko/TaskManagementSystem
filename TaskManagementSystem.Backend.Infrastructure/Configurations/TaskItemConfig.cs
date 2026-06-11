using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystem.Backend.Domain.Entities;
using TaskManagementSystem.Backend.Domain.Enums;

namespace TaskManagementSystem.Backend.Infrastructure.Configurations
{
    internal class TaskItemConfig : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Status).IsRequired().HasDefaultValue(TaskItemStatuses.TODO);
            builder.Property(u => u.Description).IsRequired().HasMaxLength(400);

            builder.HasMany(u => u.Comments).WithOne(p => p.Task).HasForeignKey(p => p.TaskId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
