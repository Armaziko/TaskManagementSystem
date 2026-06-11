using FluentValidation;
using TaskManagementSystem.Backend.Application.Queries.UserQueries;

namespace TaskManagementSystem.Backend.Application.Validators.UserValidators
{
    public class GetUserByIdValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdValidator()
        {
            RuleFor(u => u.Id).NotEmpty();
        }
    }
}