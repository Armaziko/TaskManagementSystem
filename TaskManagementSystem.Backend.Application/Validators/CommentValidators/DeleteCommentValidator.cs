using FluentValidation;
using TaskManagementSystem.Backend.Application.Commands.CommentCommands;

namespace TaskManagementSystem.Backend.Application.Validators.CommentValidators
{
    public class DeleteCommentValidator : AbstractValidator<DeleteCommentCommand>
    {
        public DeleteCommentValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
        }
    }
}
