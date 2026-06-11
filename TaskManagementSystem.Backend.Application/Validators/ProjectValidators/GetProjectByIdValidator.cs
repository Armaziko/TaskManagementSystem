using FluentValidation;
using TaskManagementSystem.Backend.Application.Queries.ProjectQueries;

namespace TaskManagementSystem.Backend.Application.Validators.ProjectValidators
{
    public   class GetProjectByIdValidator : AbstractValidator<GetProjectByIdQuery>
    {
        public GetProjectByIdValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
