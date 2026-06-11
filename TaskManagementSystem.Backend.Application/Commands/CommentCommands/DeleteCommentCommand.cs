using MediatR;
using TaskManagementSystem.Backend.Application.Models;

namespace TaskManagementSystem.Backend.Application.Commands.CommentCommands
{
    public class DeleteCommentCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
