using TaskManagementSystem.Backend.Domain.Enums;
using TaskManagementSystem.Backend.Domain.Generics;

namespace TaskManagementSystem.Backend.Domain.Entities
{
    public class TaskItem : BaseEntity
    {
        public TaskItem(Guid id) : base(id)
        {
        }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public TaskItemStatuses Status { get; set; } = TaskItemStatuses.TODO;
        public Guid ProjectId { get; set; }
        public Guid CreatorUserId { get; set; } // ვივარაუდე, რომ ამტვირთავის ვინაობაც უნდა შეინახოს.
        public Guid AssignedUserId { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // ვივარაუდე, რომ შექმნის დროს დაფიქსირება საჭიროა.
        public DateTime UpdatedAt { get; set; }
        // ნავიგაციის ფროფერთიები
        public Project Project { get; set; } = default!;
        public User AssignedUser { get; set; } = default!;
        public User CreatorUser { get; set; } = default!;
        public List<Comment> Comments { get; set; } = new();
    }
}
