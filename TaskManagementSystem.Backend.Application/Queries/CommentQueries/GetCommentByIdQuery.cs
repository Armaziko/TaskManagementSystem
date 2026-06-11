using MediatR;
using TaskManagementSystem.Backend.Application.Models;
using TaskManagementSystem.Backend.Application.Models.DTOs;

namespace TaskManagementSystem.Backend.Application.Queries.CommentQueries
{
    public class GetCommentByIdQuery : IRequest<Result<CommentDto>>
    {
        public Guid Id { get; set; }
    }
}
