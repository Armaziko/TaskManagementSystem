using TaskManagementSystem.Backend.Domain.Generics;

namespace TaskManagementSystem.Backend.Domain.Entities
{
    public class User : AggregateRoot
    {
        public User(Guid id) : base(id)
        {
        }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        // ნავიგაციის ფროფერთიები
        public List<Project> Projects { get; set; } = new();
        public List<TaskItem> CreatedTasks { get; set; } = new();
        public List<TaskItem> AssignedTasks { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();

        // Factory მეთოდი
        public static User CreateUser(string firstName, string lastName, string email)
        {
            return new User(Guid.NewGuid()) 
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };
        }
    }
}
