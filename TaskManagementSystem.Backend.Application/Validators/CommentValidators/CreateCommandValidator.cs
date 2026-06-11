using FluentValidation;
using TaskManagementSystem.Backend.Application.Commands.CommentCommands;

namespace TaskManagementSystem.Backend.Application.Validators.CommentValidators
{
    public class CreateCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(c => c.Content).NotEmpty().MaximumLength(1024);
            RuleFor(c => c.CommentMakerId).NotEmpty();
            RuleFor(c => c.TaskId).NotEmpty();
        }
    }
}
