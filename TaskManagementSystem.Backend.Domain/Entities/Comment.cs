using TaskManagementSystem.Backend.Domain.Generics;

namespace TaskManagementSystem.Backend.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public Comment(Guid id) : base(id)
        {
        }
        public string Content { get; set; } = default!;
        public Guid TaskId { get; set; }
        public Guid CommentMakerId { get; set; } // ვივარაუდე, რომ ნებისმიერ მომხმარებელს შეუძლია კომენტარის გაკეთება
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // ნავიგაციის ფროფერთიები
        public TaskItem Task { get; set; } = default!;
        public User CommentMaker { get; set; } = default!;

        public static Comment CreateProject(string content, Guid taskId, Guid commentMakerId)
        {
            return new Comment(Guid.NewGuid()) 
            {
                Content = content,
                TaskId = taskId,
                CommentMakerId = commentMakerId
            };
        }
    }
}
