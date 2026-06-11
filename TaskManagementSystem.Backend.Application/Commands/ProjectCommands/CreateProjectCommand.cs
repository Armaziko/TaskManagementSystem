using MediatR;
using TaskManagementSystem.Backend.Application.Models;

namespace TaskManagementSystem.Backend.Application.Commands.ProjectCommands
{
    public class CreateProjectCommand : IRequest<Result>
    {
        public Guid CreatorUserId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}
