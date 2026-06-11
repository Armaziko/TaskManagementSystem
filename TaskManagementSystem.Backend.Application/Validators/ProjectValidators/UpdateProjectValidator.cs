using FluentValidation;
using TaskManagementSystem.Backend.Application.Commands.ProjectCommands;

namespace TaskManagementSystem.Backend.Application.Validators.ProjectValidators
{
    public class UpdateProjectValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Name).MaximumLength(64);
            RuleFor(p => p.Description).MaximumLength(512);
        }
    }
}
