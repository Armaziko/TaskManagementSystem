using MediatR;
using TaskManagementSystem.Backend.Application.Models;
using TaskManagementSystem.Backend.Application.Models.DTOs;

namespace TaskManagementSystem.Backend.Application.Queries.CommentQueries
{
    public class GetAllCommentsQuery : IRequest<Result<IReadOnlyList<CommentDto>>>
    {
    }
}
