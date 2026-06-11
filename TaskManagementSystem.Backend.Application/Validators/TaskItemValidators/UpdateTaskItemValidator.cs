using FluentValidation;
using TaskManagementSystem.Backend.Application.Commands.TaskItemCommands;

namespace TaskManagementSystem.Backend.Application.Validators.TaskItemValidators
{
    public class UpdateTaskItemValidator : AbstractValidator<UpdateTaskItemCommand>
    {
        public UpdateTaskItemValidator()
        {
            RuleFor(u => u.Id).NotEmpty();
            RuleFor(U => U.Name).MaximumLength(64);
            RuleFor(U => U.Description).MaximumLength(1024);
        }
    }
}
