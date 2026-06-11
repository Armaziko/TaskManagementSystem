using MediatR;
using TaskManagementSystem.Backend.Application.Models;

namespace TaskManagementSystem.Backend.Application.Commands.TaskItemCommands
{
    public class DeleteTaskItemCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
