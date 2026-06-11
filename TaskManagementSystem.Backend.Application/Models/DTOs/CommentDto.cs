namespace TaskManagementSystem.Backend.Application.Models.DTOs
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = default!;
        public Guid TaskId { get; set; }
        public Guid CommentMakerId { get; set; } 
        public DateTime CreatedAt { get; set; }
    }
}
