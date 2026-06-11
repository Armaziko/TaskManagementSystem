using FluentValidation;
using TaskManagementSystem.Backend.Application.Commands.TaskItemCommands;

namespace TaskManagementSystem.Backend.Application.Validators.TaskItemValidators
{
    public class DeleteTaskItemValidator : AbstractValidator<DeleteTaskItemCommand>
    {
        public DeleteTaskItemValidator()
        {
            RuleFor(u => u.Id).NotEmpty();
        }
    }
}
