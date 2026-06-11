using FluentValidation;
using TaskManagementSystem.Backend.Application.Commands.UserCommands;

namespace TaskManagementSystem.Backend.Application.Validators.UserValidators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(c => c.FirstName).MaximumLength(64);
            RuleFor(c => c.LastName).MaximumLength(64);
            RuleFor(c => c.Email).EmailAddress();
        }
    }
}
