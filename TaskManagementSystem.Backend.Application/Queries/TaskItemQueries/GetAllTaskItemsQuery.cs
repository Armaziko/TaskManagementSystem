using MediatR;
using TaskManagementSystem.Backend.Application.Models;
using TaskManagementSystem.Backend.Application.Models.DTOs;

namespace TaskManagementSystem.Backend.Application.Queries.TaskItemQueries
{
    public class GetAllTaskItemsQuery : IRequest<Result<IReadOnlyList<TaskItemDto>>>
    {
    }
}
