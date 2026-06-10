using TaskManagementSystem.Backend.Domain.Generics;

namespace TaskManagementSystem.Backend.Domain.Entities
{
    public class Project : AggregateRoot
    {
        public Project(Guid id) : base(id)
        {
        }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public Guid CreatorUserId { get; set; } // ვივარაუდე, რომ შემქმნელის ვინაობა უნდა იქნეს დამახსოვრებული
        // ნავიგაციის ფროფერთიები
        public User CreatorUser { get; set; } = default!;
        public List<TaskItem> Tasks { get; set; } = new();
    }
}
