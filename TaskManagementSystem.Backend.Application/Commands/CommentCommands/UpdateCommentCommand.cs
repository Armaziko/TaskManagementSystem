using MediatR;
using TaskManagementSystem.Backend.Application.Models;
using TaskManagementSystem.Backend.Application.Models.DTOs;

namespace TaskManagementSystem.Backend.Application.Commands.CommentCommands
{
    public class UpdateCommentCommand : IRequest<Result<CommentDto>>
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = default!;
    }
}
