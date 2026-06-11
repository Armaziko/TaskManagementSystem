using FluentValidation;
using TaskManagementSystem.Backend.Application.Commands.CommentCommands;

namespace TaskManagementSystem.Backend.Application.Validators.CommentValidators
{
    public class UpdateCommentValidator : AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentValidator()
        {
            RuleFor(c => c.Content).NotEmpty().MaximumLength(1024);
            RuleFor(c => c.Id).NotEmpty();
        }
    }
}
