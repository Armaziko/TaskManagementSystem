using FluentValidation;
using TaskManagementSystem.Backend.Application.Queries.TaskItemQueries;

namespace TaskManagementSystem.Backend.Application.Validators.TaskItemValidators
{
    public class GetTaskItemByIdValidator : AbstractValidator<GetTaskItemByIdQuery>
    {
        public GetTaskItemByIdValidator()
        {
            RuleFor(u => u.Id).NotEmpty();
        }
    }
}
