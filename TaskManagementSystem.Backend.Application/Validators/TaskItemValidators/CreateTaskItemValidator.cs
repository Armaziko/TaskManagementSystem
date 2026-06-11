using FluentValidation;
using TaskManagementSystem.Backend.Application.Commands.TaskItemCommands;

namespace TaskManagementSystem.Backend.Application.Validators.TaskItemValidators
{
    public class CreateTaskItemValidator : AbstractValidator<CreateTaskItemCommand>
    {
        public CreateTaskItemValidator()
        {
            RuleFor(ti => ti.CreatorUserId).NotEmpty();
            RuleFor(ti => ti.ProjectId).NotEmpty();
            RuleFor(ti => ti.AssignedUserId).NotEmpty();
            RuleFor(ti => ti.Title).NotEmpty().MaximumLength(64);
            RuleFor(ti => ti.Description).NotEmpty().MaximumLength(1024);
        }
    }
}
