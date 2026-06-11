using MediatR;
using TaskManagementSystem.Backend.Application.Models;

namespace TaskManagementSystem.Backend.Application.Commands.ProjectCommands
{
    public class DeleteProjectCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
