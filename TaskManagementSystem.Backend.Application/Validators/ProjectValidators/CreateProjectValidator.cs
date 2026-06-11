using FluentValidation;
using TaskManagementSystem.Backend.Application.Commands.ProjectCommands;

namespace TaskManagementSystem.Backend.Application.Validators.ProjectValidators
{
    public class CreateProjectValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(64);
            RuleFor(p => p.Description).NotEmpty().MaximumLength(100);
            RuleFor(p => p.CreatorUserId).NotEmpty();
        }
    }
}
