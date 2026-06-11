using MediatR;
using TaskManagementSystem.Backend.Application.Models;

namespace TaskManagementSystem.Backend.Application.Commands.TaskItemCommands
{
    public class CreateTaskItemCommand : IRequest<Result>
    {
        public Guid CreatorUserId { get; set; }
        public Guid AssignedUserId { get; set; }
        public Guid ProjectId { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}
