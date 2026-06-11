using MediatR;
using TaskManagementSystem.Backend.Application.Models;
using TaskManagementSystem.Backend.Application.Models.DTOs;

namespace TaskManagementSystem.Backend.Application.Commands.ProjectCommands
{
    public class UpdateProjectCommand : IRequest<Result<ProjectDto>>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
