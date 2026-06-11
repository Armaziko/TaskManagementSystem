using MediatR;
using TaskManagementSystem.Backend.Application.Models;
using TaskManagementSystem.Backend.Application.Models.DTOs;

namespace TaskManagementSystem.Backend.Application.Commands.TaskItemCommands
{
    public class UpdateTaskItemCommand : IRequest<Result<TaskItemDto>>
    {
        public Guid Id { get; set; }
        public Guid? AssignedUserId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
