using FluentValidation;
using TaskManagementSystem.Backend.Application.Commands.ProjectCommands;

namespace TaskManagementSystem.Backend.Application.Validators.ProjectValidators
{
    public class DeleteProjectValidator : AbstractValidator<DeleteProjectCommand>
    {
        public DeleteProjectValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
