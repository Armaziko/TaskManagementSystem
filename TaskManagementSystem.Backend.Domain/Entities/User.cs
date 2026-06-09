using TaskManagementSystem.Backend.Domain.Generics;

namespace TaskManagementSystem.Backend.Domain.Entities
{
    public class User : BaseEntity
    {
        public User(Guid id) : base(id)
        {
        }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;

        // ნავიგაციის ფროფერთიები
        public List<Project> Projects { get; set; } = new();
        public List<TaskItem> CreatedTasks { get; set; } = new();
        public List<TaskItem> AssignedTasks { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
    }
}
