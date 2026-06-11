using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystem.Backend.Domain.Entities;

namespace TaskManagementSystem.Backend.Infrastructure.Configurations
{
    public class ProjectConfig : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.Name).IsUnique();
            builder.Property(u => u.Name).IsRequired();
            builder.Property(u => u.Description).IsRequired();

            builder.HasMany(u => u.Tasks).WithOne(p => p.Project).HasForeignKey(p => p.ProjectId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
