using MediatR;
using TaskManagementSystem.Backend.Application.Models;

namespace TaskManagementSystem.Backend.Application.Commands.CommentCommands
{
    public class CreateCommentCommand : IRequest<Result>
    {
        public Guid CommentMakerId { get; set; }
        public Guid TaskId { get; set; }
        public string Content { get; set; } = default!;
    }
}
