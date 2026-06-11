namespace TaskManagementSystem.Backend.Application.Models.DTOs
{
    public class TaskItemDto
    {
        public Guid Id { get; set; }
        public Guid AssignedUserId { get; set; }
        public Guid CreatorUserId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
