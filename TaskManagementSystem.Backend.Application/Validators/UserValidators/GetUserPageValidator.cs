using FluentValidation;
using TaskManagementSystem.Backend.Application.Queries.UserQueries;

namespace TaskManagementSystem.Backend.Application.Validators.UserValidators
{
    public class GetUserPageValidator : AbstractValidator<GetUserPageQuery>
    {
        public GetUserPageValidator()
        {
            RuleFor(u => u.Page).Must(n => n > 0);
            RuleFor(u => u.ItemLimit).Must(n => n > 0 || n == null);
        }
    }
}
