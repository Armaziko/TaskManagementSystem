using MediatR;
using TaskManagementSystem.Backend.Application.Models;
using TaskManagementSystem.Backend.Application.Models.DTOs;

namespace TaskManagementSystem.Backend.Application.Queries.ProjectQueries
{
    public class GetProjectByIdQuery : IRequest<Result<ProjectDto>>
    {
        public Guid Id { get; set; }
    }
}